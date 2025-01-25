using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    [SerializeField] private float sprint = 1.5f;
    [SerializeField] private float crouch = .5f;
    public float movespeed = 0f;
    public float forceMultiplyer = 30000f;
    private Rigidbody2D rigidbody;
    private Vector2 movementDirection;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Basic Movement Input WASD/arrows
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // Set move speed to standard speed
        movespeed = forceMultiplyer;

        // When clicking and holding mouse1 should enter sprint
        if (Input.GetMouseButton(0))
        {
            movespeed = forceMultiplyer * sprint;
        }
        // When clicking and holding mouse2 should enter crouch
        if (Input.GetMouseButton(1))
        {
            movespeed = forceMultiplyer * crouch;
        }
    }

    private void FixedUpdate()
    {
        rigidbody.AddForce(movementDirection * movespeed, ForceMode2D.Force);
    }
}
