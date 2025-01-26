using UnityEngine;

[CreateAssetMenu(fileName = "BubbleTypeSO", menuName = "Scriptable Objects/BubbleTypeSO")]
public class BubbleTypeSO : ScriptableObject
{
    [System.Serializable]
    public struct BubbleColorEntry
    {
        public BubbleTypeEnum type;
        public Color color;
    }

    public BubbleColorEntry[] bubbleColors;

    public Color GetColor(BubbleTypeEnum type)
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
