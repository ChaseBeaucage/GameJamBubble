using UnityEngine;
using UnityEditor;
[RequireComponent(typeof(SpriteRenderer))]
public class BubbleTypeSetter : MonoBehaviour
{
    public BubbleType bubbleType;
    public BubbleTypeSO bubbleTypeSO;

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

        if (bubbleTypeSO != null && spriteRenderer != null)
        {
            spriteRenderer.color = bubbleTypeSO.GetColor(bubbleType);
        }
    }
}

public enum BubbleType
{
    Blue,
    Red,
    Green
};