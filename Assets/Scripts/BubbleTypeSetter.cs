using UnityEngine;
using UnityEditor;
using NUnit.Framework;

[RequireComponent(typeof(SpriteRenderer))]
public class BubbleTypeSetter : MonoBehaviour
{
    [SerializeField]
    private BubbleTypeEnum bubbleType;
    public BubbleTypeEnum BubbleType
    {
        get => bubbleType;
        set
        {
            bubbleType = value;
            UpdateProperties();
        }
    }
    [SerializeField]
    private BubbleSize bubbleSize;
    public BubbleSize BubbleSize
    {
        get => bubbleSize;
        set
        {
            bubbleSize = value;
            UpdateProperties();
        }
    }
    [SerializeField] private BubbleTypeSO bubbleTypeSO;
    [SerializeField] private BubbleSizeSO bubbleSizeSO;

    private bool isNPC = false;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private CircleCollider2D circleCollider2D;

    private PathAgent pathAgent;

    private void Awake()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        if (circleCollider2D == null)
        {
            circleCollider2D = GetComponent<CircleCollider2D>();
        }

        //if parent has PathAgent component, then this is an NPC bubble
        if (transform.parent != null && transform.parent.TryGetComponent<PathAgent>(out PathAgent pathAgent))
        {
            isNPC = true;
            this.pathAgent = pathAgent;
        }
    }



    public void OnValidate()
    {

        UpdateProperties();

        if (transform.parent != null && transform.parent.TryGetComponent<PathAgent>(out PathAgent pathAgent))
        {
            isNPC = true;
            this.pathAgent = pathAgent;
        }
    }

    private void UpdateProperties()
    {         
        if (bubbleTypeSO != null && spriteRenderer != null)
        {
            spriteRenderer.color = bubbleTypeSO.GetColor(bubbleType);
        }

        if (bubbleSizeSO != null)
        {
            SetRadius(bubbleSizeSO.GetSize(bubbleSize));
        }
    }

    private void SetRadius(float radius)
    {
        transform.localScale = new Vector3(radius, radius, 1);
    }
}

public enum BubbleTypeEnum
{
    Blue,
    Red,
    Green
};

public enum BubbleSize
{
    XSmall,
    Small,
    Medium,
    Large,
    XLarge,
    XXLarge
};