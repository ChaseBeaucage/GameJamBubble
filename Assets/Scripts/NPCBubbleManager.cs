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

    public bool hasBookEventTriggered = false;

    private bool isAttracted = false;
    // Holds reference to player only when in bubble
    private Transform playerBubbleTransform;

    private BubbleTypeEnum bubbleType;
    private BubbleSize bubbleSize;
    private Transform parent;

    public Vector3 standard = new Vector3(0, 0, 0);
    public Vector3 crouching = new Vector3(0, 0, 0);

    private void Awake()
    {
        bubbleTypeSetter = GetComponent<BubbleTypeSetter>();
        bubbleType = bubbleTypeSetter.BubbleType;
        bubbleSize = bubbleTypeSetter.BubbleSize;
        pathAgent = transform.parent.GetComponent<PathAgent>();
        parent = transform.parent;
        parentRb = parent.GetComponent<Rigidbody2D>();
    }


    public void Update()
    {
        //if (hasBookEventTriggered)
        //{
        //    isAttracted = false;
        //    bubbleType = BubbleTypeEnum.Blue;
        //    bubbleSize = BubbleSize.Small;
        //    //transform.localScale = crouching;
        //}
        //if (!hasBookEventTriggered)
        //{
        //    isAttracted = true;
        //    bubbleType = BubbleTypeEnum.Red;
        //    bubbleSize = BubbleSize.Large;
        //    //transform.localScale = standard;
        //}
    }
    public void BookEventToggle()
    {
        hasBookEventTriggered = !hasBookEventTriggered;

        if (hasBookEventTriggered)
        {
            isAttracted = false;
            bubbleType = BubbleTypeEnum.Blue;
            bubbleSize = BubbleSize.Small;
        }
        if (!hasBookEventTriggered)
        {
            isAttracted = true;
            bubbleType = BubbleTypeEnum.Red;
            bubbleSize = BubbleSize.Large;
        }
    }

    public void SetBubbleType(BubbleTypeEnum bubbleType)
    {
        this.bubbleType = bubbleType;
    }
    public void SetBubbleSize(BubbleSize bubbleSize)
    {
        this.bubbleSize = bubbleSize;
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
                if (bubbleTypeSetter.BubbleType == BubbleTypeEnum.Blue && bubbleType == BubbleTypeEnum.Red)
                {
                    //Get the PathAgent component from the parent
                    if (pathAgent != null)
                    {
                        pathAgent.PausePathing();
                    }
                    playerBubbleTransform = collision.transform;
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
                if (bubbleTypeSetter.BubbleType == BubbleTypeEnum.Blue && bubbleType == BubbleTypeEnum.Red)
                {
                    if (pathAgent != null)
                    {
                        pathAgent.ResumePathing();
                    }
                    playerBubbleTransform = null;
                    isAttracted = false;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (isAttracted && playerBubbleTransform != null)
        {
            ApplyGravitationalPull(playerBubbleTransform);
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
