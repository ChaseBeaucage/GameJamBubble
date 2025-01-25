using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{

    public PlayableDirector timeline;
    public GameObject startMenu;
    public GameObject cutSceneStuff;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cutSceneStuff.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartPress()
    {
        startMenu.SetActive(false);
        cutSceneStuff.SetActive(true);
        timeline.Play();
    }

    public void EndScene()
    {
        // SceneManager.LoadScene()
        Debug.Log("test");
    }
}
