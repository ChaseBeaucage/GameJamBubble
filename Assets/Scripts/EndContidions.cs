using UnityEngine;
using UnityEngine.SceneManagement;

public class EndContidions : MonoBehaviour
{
    public Collider2D playersColl;
    public GameObject letter;

    public bool win = false;

    private bool inRange = false;

    private void Start()
    {
        playersColl = GetComponent<Collider2D>();
    }

    // Reach End
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && inRange)
        {
            Debug.Log("You Win");
            win = true;
            SceneManager.LoadScene("EndState");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == letter.GetComponent<Collider2D>())
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }
    }


    // Collide with red bubbled npc body
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<PathAgent>() && other.gameObject.GetComponentInChildren<BubbleTypeSetter>().bubbleType == BubbleType.Red)
        {
            Debug.Log("You Lose");
            SceneManager.LoadScene("EndState");
        }
    }

}
