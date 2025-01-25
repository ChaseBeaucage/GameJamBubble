using UnityEngine;
using UnityEngine.Events;

public class BookshelfObj : MonoBehaviour
{
    //public UnityEvent bookshelfEvent;

    public PathNode PathNode;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //bookshelfEvent.Invoke();
            //bookshelfEvent.AddListener(BookshelfEvent);
            //if (PathNode != null)
            //{
            //    PathNode.nodeEvent.Invoke();
            //    Debug.Log("PathNode event invoked!");
            //}
            BookshelfEvent();
        }
    }

    public void BookshelfEvent()
    {
        //bookshelfEvent.Invoke();
        Debug.Log("Bookshelf event invoked!");
    }
}
