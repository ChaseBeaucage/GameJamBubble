using UnityEngine;
using TMPro;

public class TextBubbles : MonoBehaviour
{
    public TMP_Text textToPrint;
    private string farSpeach = "";
    private string closeSpeach = ". . .";
    [TextArea]
    public string talkingSpeach = "This is me talking wow";

    private bool inRange = false;
    public Collider2D playerBubble;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == playerBubble)
        {
            inRange = true;
            textToPrint.text = closeSpeach;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == playerBubble)
        {
            inRange = false;
            textToPrint.text = farSpeach;
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && inRange)
        {
            textToPrint.text = talkingSpeach;
        }
    }
}
