using UnityEngine;
using UnityEditor;

public class BubbleManager : MonoBehaviour
{
    public BubbleType bubbleType;

    public Color redBubble;
    public Color blueBubble;
    public Color greenBubble;

    [SerializeField] private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnValidate()
    {
        // SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        switch (bubbleType)
        {
            case BubbleType.Red:
                spriteRenderer.color = redBubble;
                break;
            case BubbleType.Blue:
                spriteRenderer.color = blueBubble;
                break;
            case BubbleType.Green:
                spriteRenderer.color = greenBubble;
                break;
            default:
                spriteRenderer.color = redBubble;
                break;
        }
    }
}

public enum BubbleType
{
    Blue,
    Red,
    Green
};