using UnityEditor;
using UnityEngine;

/// <summary>
/// This Script is taken from Chat-GPT to add a custom selector of all the Tags there is in the project for other scripts to use.
/// you will be able to select tag in inspector.
/// exempel : 
///         [TagSelector]
///         public string selectedTag;
///         
/// </summary>
[CustomPropertyDrawer(typeof(TagSelectorAttribute))]
public class TagSelectorDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType == SerializedPropertyType.String)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Get the list of tags
            string[] tags = UnityEditorInternal.InternalEditorUtility.tags;

            // Find the current tag in the list
            int index = System.Array.IndexOf(tags, property.stringValue);

            // Ensure index is valid
            if (index == -1) index = 0;

            // Display a popup
            index = EditorGUI.Popup(position, label.text, index, tags);

            // Update the value in the property
            property.stringValue = tags[index];

            EditorGUI.EndProperty();
        }
        else
        {
            EditorGUI.PropertyField(position, property, label);
        }
    }
}