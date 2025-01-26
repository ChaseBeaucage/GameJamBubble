using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    [Header("Movement Multipliers")]
    [SerializeField] private float sprintMultiplier = 1.5f;
    [SerializeField] private float crouchMultiplier = 0.5f;
    [SerializeField] private float baseMoveSpeed = 1f;

    [Header("Force Settings")]
    [SerializeField] private float forceMultiplier = 300f;

    [Header("Drag Settings")]
    [Tooltip("Drag applied when there's no movement input.")]
    [SerializeField] private float idleDrag = 10f;
    [Tooltip("Drag applied when there is movement input.")]
    [SerializeField] private float movingDrag = 0f;

    private Rigidbody2D rb;
    private Vector2 movementDirection;

    public bool isSprinting { get; private set; }
    public bool isCrouching { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // Set an initial drag.
        rb.linearDamping = movingDrag;
    }

    private void Update()
    {
        // 1. Gather raw movement input.
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        movementDirection = new Vector2(xInput, yInput);

        // 2. Determine sprint / crouch states.
        isSprinting = Input.GetMouseButton(0);
        isCrouching = Input.GetMouseButton(1);
    }

    private void FixedUpdate()
    {
        // 1. Determine final speed factor
        float speedFactor = baseMoveSpeed;
        if (isSprinting) speedFactor *= sprintMultiplier;
        if (isCrouching) speedFactor *= crouchMultiplier;

        // 2. If there's movement input, apply force and use 'movingDrag'.
        if (movementDirection.sqrMagnitude > 0.01f)
        {
            rb.linearDamping = movingDrag;

            // Normalize so diagonal movement isn’t faster, then multiply by your force.
            Vector2 force = movementDirection.normalized * speedFactor * forceMultiplier * Time.fixedDeltaTime;
            rb.AddForce(force);
        }
        else
        {
            // 3. If no movement input, increase drag to slow down quickly.
            rb.linearDamping = idleDrag;
        }
    }
}