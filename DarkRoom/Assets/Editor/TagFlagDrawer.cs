using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

/// <summary>
/// This Script is taken from Chat-GPT to add a custom Flag of all the Tags there is in the project for other scripts to use.
/// you will be able to select tag in inspector.
/// exempel : 
///         [TagFlag]
///         public string selectedTags;
///         
///         void Start()
///         {
///         Split selected tags into an array for runtime use  
///          string[] tags = selectedTags.Split(',');
///         Debug.Log($"Selected tags: {string.Join(", ", tags)}");
///         
///         }
///         
/// </summary>

[CustomPropertyDrawer(typeof(TagFlagAttribute))]
public class TagFlagDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType == SerializedPropertyType.String)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Get all tags
            string[] tags = UnityEditorInternal.InternalEditorUtility.tags;

            // Create a list of boolean flags for the tags
            bool[] selectedTags = new bool[tags.Length];
            string currentValue = property.stringValue;
            string[] currentTags = string.IsNullOrEmpty(currentValue) ? new string[0] : currentValue.Split(',');

            // Determine currently selected tags
            for (int i = 0; i < tags.Length; i++)
            {
                selectedTags[i] = Array.Exists(currentTags, tag => tag == tags[i]);
            }

            // Display the tag toggle group
            EditorGUILayout.LabelField(label.text);
            for (int i = 0; i < tags.Length; i++)
            {
                selectedTags[i] = EditorGUILayout.Toggle(tags[i], selectedTags[i]);
            }

            // Update the property value based on selected tags
            property.stringValue = string.Join(",", tags.Where((tag, index) => selectedTags[index]));

            EditorGUI.EndProperty();
        }
        else
        {
            EditorGUI.LabelField(position, label.text, "Use [TagFlag] with a string field.");
        }
    }
}
