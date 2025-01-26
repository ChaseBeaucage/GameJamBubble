using UnityEngine;
using UnityEngine.Events;

public class BookshelfObj : MonoBehaviour
{
    //public UnityEvent bookshelfEvent;

    public PathNode PathNode;

    public TextBubbles bubble;

    public NPCBubbleManager npcManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && bubble.inRange == true)
        {
            //bookshelfEvent.Invoke();
            //bookshelfEvent.AddListener(BookshelfEvent);


            Debug.Log("PathNode event invoked!");

            npcManager.BookEventToggle();

            PathNode.TriggerEvent();
        }


    }


    public void BookshelfEvent()
    {
        //bookshelfEvent.Invoke();
        Debug.Log("Bookshelf event invoked!");

    }
}