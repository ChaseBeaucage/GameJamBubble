using UnityEngine;
public class PlayerBubbleManager : MonoBehaviour
{
    public CharacterMovement characterMovement;
    [SerializeField] private BubbleTypeSetter bubbleTypeSetter;

    private void OnValidate()
    {
        if (characterMovement == null)
        {
            characterMovement = transform.parent.GetComponent<CharacterMovement>();
        }
        if (bubbleTypeSetter == null)
        {
            bubbleTypeSetter = GetComponent<BubbleTypeSetter>();
        }
    }
    private void Start()
    {
    }

    private void Update()
    {
        if (characterMovement.isCrouching)
        {
            bubbleTypeSetter.BubbleSize = BubbleSize.Small;
        }
        else if (characterMovement.isSprinting)
        {
            bubbleTypeSetter.BubbleSize = BubbleSize.Large;
        }
        else
        {
            bubbleTypeSetter.BubbleSize = BubbleSize.Medium;
        }
    }
}
