using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CutsceneAnimationEvents : MonoBehaviour
{
    public TMP_Text dialogueText;
    private int textCounter;

    [TextArea]
    public string[] textArray;
    
   public void ChangeText()
    {
        dialogueText.text = textArray[textCounter];
        textCounter++;
    }

    void EndScene()
    {
        // SceneManager.LoadScene()
        print("test");
    }
}
