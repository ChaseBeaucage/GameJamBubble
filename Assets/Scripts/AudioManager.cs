using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource audioSource;

    public AudioClip bubblePop;
    public AudioClip bookShelfOne;
    public AudioClip bookShelfTwo;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayPop()
    {
        audioSource.PlayOneShot(bubblePop);
    } 

    public void PlayBookShelf()
    {
        int rand = Random.Range(0, 2);

        if (rand == 0)
        {
            audioSource.PlayOneShot(bookShelfOne);
        } else
        {
            audioSource.PlayOneShot(bookShelfTwo);
        }
    
    }
}
