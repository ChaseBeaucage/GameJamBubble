using UnityEngine;
using TMPro;

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
}
