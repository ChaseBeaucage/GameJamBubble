using UnityEngine;

[CreateAssetMenu(fileName = "BubbleTypeSO", menuName = "Scriptable Objects/BubbleTypeSO")]
public class BubbleTypeSO : ScriptableObject
{
    [System.Serializable]
    public struct BubbleColorEntry
    {
        public BubbleType type;
        public Color color;
    }

    public BubbleColorEntry[] bubbleColors;

    public Color GetColor(BubbleType type)
    {
        foreach (var entry in bubbleColors)
        {
            if (entry.type == type)
            {
                return entry.color;
            }
        }
        return Color.white; // Default fallback color
    }
}
