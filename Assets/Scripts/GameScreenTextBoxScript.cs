
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum  States{
    NotExistingText,
    CorrectlyPositionedText,
    CorrectEnteredButWrongPositionText,
    None
}
public class 
    GameScreenTextBoxScript : MonoBehaviour
{
    public States state;
    public TMP_Text text;
    public Image image;
}
