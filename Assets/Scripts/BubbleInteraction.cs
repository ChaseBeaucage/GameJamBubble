using UnityEngine;
public class BubbleInteraction : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    double baseSpeed;
    public CharacterMovement characterMovement;
    public GameObject bubble;
    public Vector3 standard = new Vector3(0, 0, 0);
    public Vector3 crouching = new Vector3(0, 0, 0);
    public Vector3 sprinting = new Vector3(0, 0, 0);
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        baseSpeed = characterMovement.forceMultiplyer;
    }

    private void Update()
    {
        if (characterMovement.movespeed > baseSpeed)
        {
            bubble.transform.localScale = sprinting;
        }
        if (characterMovement.movespeed < baseSpeed)
        {
            bubble.transform.localScale = crouching;
        }
        if (characterMovement.movespeed == baseSpeed)
        {
            bubble.transform.localScale = standard;
        }
    }
}
