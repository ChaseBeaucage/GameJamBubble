using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BubbleSizeSO", menuName = "Scriptable Objects/BubbleSizeSO")]
public class BubbleSizeSO : ScriptableObject
{
    [Serializable]
    public struct BubbleSizeEntry
    {
        public BubbleSize size;
        public float radius;
    }

    public BubbleSizeEntry[] bubbleSizes;

    public float GetSize(BubbleSize size)
    {
        foreach (var entry in bubbleSizes)
        {
            if (entry.size == size)
            {
                return entry.radius;
            }
        }
        return 0; // Default fallback size
    }   
}
