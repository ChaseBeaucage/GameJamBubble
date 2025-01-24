using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [SerializeField] private float acceleration = 50000f;

    private Rigidbody2D rigidbody;

    private Vector2 movementDirection;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Movement commented out awaiting coordinate based movement system + map design
        //movementDirection = (new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
    }

    private void FixedUpdate()
    {
        rigidbody.AddForce(movementDirection * acceleration, ForceMode2D.Force);
    }
}
