using UnityEngine;

public class NPCBubbleManager : MonoBehaviour
{
    [Tooltip("Strength of gravitational pull toward the Player when bubbles overlap.")]
    public float gravityStrength = 10f;
    [Tooltip("Max cap on the gravitational force (optional).")]
    public float maxGravityForce = 50f;

    private Rigidbody2D parentRb;
    [SerializeField] private BubbleTypeSetter bubbleTypeSetter;
    [SerializeField] private PathAgent pathAgent;

    private bool isAttracted = false;
    private Transform playerTransform;

    private BubbleType bubbleType;
    private Transform parent;

    private void Awake()
    {
        bubbleTypeSetter = GetComponent<BubbleTypeSetter>();
        bubbleType = bubbleTypeSetter.bubbleType;
        pathAgent = transform.parent.GetComponent<PathAgent>();
        parent = transform.parent;
        parentRb = parent.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.TryGetComponent<BubbleTypeSetter>(out BubbleTypeSetter bubbleTypeSetter))
        {
            if (collision.gameObject.TryGetComponent<NPCBubbleManager>(out NPCBubbleManager npcBubbleManager))
            {
                Debug.Log("NPC bubble collided with NPC bubble");
            }
            else
            {
                Debug.Log("NPC bubble collided with player bubble");
                if (bubbleTypeSetter.bubbleType == BubbleType.Blue && bubbleType == BubbleType.Red)
                {
                    //Get the PathAgent component from the parent
                    if (pathAgent != null)
                    {
                        pathAgent.PausePathing();
                    }
                    playerTransform = collision.transform;
                    isAttracted = true;
                }
            
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<BubbleTypeSetter>(out BubbleTypeSetter bubbleTypeSetter))
        {
            if (collision.gameObject.TryGetComponent<NPCBubbleManager>(out NPCBubbleManager npcBubbleManager))
            {
                Debug.Log("NPC bubble exited NPC bubble");
            }
            else
            {
                Debug.Log("NPC bubble exited player bubble");
                if (bubbleTypeSetter.bubbleType == BubbleType.Blue && bubbleType == BubbleType.Red)
                {
                    if (pathAgent != null)
                    {
                        pathAgent.ResumePathing();
                    }
                    playerTransform = null;
                    isAttracted = false;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (isAttracted && playerTransform != null)
        {
            ApplyGravitationalPull(playerTransform);
        }
    }

    /// <summary>
    /// Applies a "gravitational" force toward the player, stronger when closer.
    /// </summary>
    private void ApplyGravitationalPull(Transform playerTransform)
    {
        if (playerTransform == null) return;

        float dist = Vector2.Distance(transform.position, playerTransform.position);
        // Avoid division by zero
        dist = Mathf.Max(dist, 0.01f);

        // Example: F ~ gravityStrength / dist^2 
        float forceMagnitude = gravityStrength / (dist * dist);
        forceMagnitude = Mathf.Min(forceMagnitude, maxGravityForce);

        Vector2 dirToPlayer = (playerTransform.position - transform.position).normalized;
        parentRb.AddForce(dirToPlayer * forceMagnitude);
    }
}
