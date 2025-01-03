using UnityEngine;
using UnityEditor;

/// <summary>
/// This Script is taken from Chat-GPT to add a custom Mask of all the Tags there is in the project for other scripts to use.
/// you will be able to select tag in inspector.
/// exempel : 
/// public TagMask tagMask;
///
/// private void OnCollisionEnter(Collision collision)
/// {
///       if (tagMask.Contains(collision.gameObject))
///     {
///        Debug.Log($"Collided with a tagged object: {collision.gameObject.tag}");
///     }
///     else
///     {
///        Debug.Log("Collided with an untagged or irrelevant object.");
///     }
/// } 
/// </summary>

[CustomPropertyDrawer(typeof(TagMask))]
public class TagMaskDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Get the TagMask and the mask integer
        SerializedProperty maskProperty = property.FindPropertyRelative("mask");

        // Initialize the tag list in the Editor
#if UNITY_EDITOR
        TagMask tagMask = (TagMask)fieldInfo.GetValue(property.serializedObject.targetObject);
        tagMask.InitializeTagList();  // Initialize the tags only in the Editor
#endif

        // Get all available tags in Unity (only in Editor)
        string[] tags = UnityEditorInternal.InternalEditorUtility.tags;

        // Create a dropdown for the user to select multiple tags
        int currentMask = maskProperty.intValue;

        // Use the MaskField for multi-selection
        currentMask = EditorGUI.MaskField(position, label, currentMask, tags);

        // Store the updated mask value
        maskProperty.intValue = currentMask;

        EditorGUI.EndProperty();
    }
}