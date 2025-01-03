using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TagMask
{
    public int mask; // Integer to store the bitmask

    // Initialize tagList only in the Editor
    private string[] tagList;

    // Constructor
    public TagMask()
    {
        // For runtime, initialize it as an empty array
        tagList = new string[0];
    }

    // Initialize tagList in the Editor
#if UNITY_EDITOR
    public void InitializeTagList()
    {
        // Initialize tagList only in the Editor
        tagList = UnityEditorInternal.InternalEditorUtility.tags;
    }
#endif

    public bool Contains(string tag)
    {
        int index = System.Array.IndexOf(tagList, tag);
        return index >= 0 && (mask & (1 << index)) != 0;
    }

    public bool Contains(GameObject obj)
    {
        return Contains(obj.tag);
    }

    // Add a tag to the mask
    public void Add(string tag)
    {
        int index = System.Array.IndexOf(tagList, tag);
        if (index >= 0)
        {
            mask |= (1 << index);
        }
    }

    // Remove a tag from the mask
    public void Remove(string tag)
    {
        int index = System.Array.IndexOf(tagList, tag);
        if (index >= 0)
        {
            mask &= ~(1 << index);
        }
    }

    // Convert the mask to a human-readable string
    public override string ToString()
    {
        List<string> selectedTags = new List<string>();
        for (int i = 0; i < tagList.Length; i++)
        {
            if ((mask & (1 << i)) != 0)
            {
                selectedTags.Add(tagList[i]);
            }
        }
        return string.Join(", ", selectedTags);
    }
}
