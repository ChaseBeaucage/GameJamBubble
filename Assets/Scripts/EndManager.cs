using UnityEngine;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    void Start()
    {

    }

    public void OnReplayPress()
    {
        SceneManager.LoadScene("Library");
    }
}
