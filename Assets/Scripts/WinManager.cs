using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    void Start()
    {

    }

    public void OnReplayPress()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
