using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float acceleration = 55000f;
    [SerializeField] private float sprint = 1.5f;
    [SerializeField] private float movespeed = 0f;

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

        // When clicking and holding mouse1 should enter sprint

        //DOES NOT WORK SHOULD IMPLEMENT CROUCH TO M2
        if (Input.GetMouseButtonDown(0))
        {
            movespeed = acceleration * sprint;
        }
        else
        {
            movespeed = acceleration;
        }

    }

    private void FixedUpdate()
    {
        rigidbody.AddForce(movementDirection * movespeed, ForceMode2D.Force);
    }
}
