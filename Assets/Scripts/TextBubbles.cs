using UnityEngine;
using TMPro;

public class TextBubbles : MonoBehaviour
{
    public TMP_Text textToPrint;
    private string farSpeach = "";
    private string closeSpeach = ". . .";
    [TextArea]
    public string talkingSpeach = "";

    public bool inRange = false;
    [SerializeField] private BoxCollider2D triggerArea;
    //public Collider2D playerBubble;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        //GameObject player = oth
        //other.TryGetComponent<CharacterMovement>(out CharacterMovement characterMovement)
        //if ()
        //{
        //    inRange = true;
        //    textToPrint.text = closeSpeach;
        //}

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //if (other.TryGetComponent<CharacterMovement>(out CharacterMovement characterMovement))
        //{
        //    inRange = false;
        //    textToPrint.text = farSpeach;
        //}

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && inRange)
        {
            textToPrint.text = talkingSpeach;
        }
    }
}