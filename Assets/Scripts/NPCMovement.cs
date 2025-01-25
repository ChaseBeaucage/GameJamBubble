using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [SerializeField] private float acceleration = 50000f;



    private Rigidbody2D rigidbody;

    private Vector2 movementDirection;

    public int directionMod = 1;

    private double startTime;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        startTime = Time.timeAsDouble;
    }

    private void Update()
    {
        while (Time.timeAsDouble - startTime >= 2)
        {
            directionMod *= -1;
            startTime = Time.timeAsDouble;
        }
        movementDirection = new Vector2(1 * directionMod, 0);
    }

    private void FixedUpdate()
    {
        rigidbody.AddForce(movementDirection * acceleration, ForceMode2D.Force);
    }




}
