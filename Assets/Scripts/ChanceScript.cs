
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChanceScript : MonoBehaviour
{
    [SerializeField] private TMP_Text[] textItems = new TMP_Text[5];

    public void SetText(string enteredAlphabet,int index)
    {
        textItems[index].text = enteredAlphabet;
    }

    public string GetString()
    {
        string enteredString = "";
        for (int i = 0; i < 5; i++)
        {
            enteredString += textItems[i].text;
        }

        return enteredString;
    }

    public void ColorTheString(string solutionString)
    {
        solutionString = solutionString.ToUpper();
        for (int i = 0; i < 5; i++)
        {
            Image currentTextsImageReference = textItems[i].gameObject.GetComponentInParent<Image>();
            if (textItems[i].text[0] == solutionString[i])
            {
                currentTextsImageReference.color = Constants.GreenColor;
                textItems[i].color = Constants.WhiteColor;

            }
            else if (!solutionString.Contains(textItems[i].text))
            {
                currentTextsImageReference.color = Constants.RedColor;
                textItems[i].color = Constants.WhiteColor;

                //disable this key

                gameObject.GetComponentInParent<GameScreenScript>().DisableKeyBoardKey(textItems[i].text);
            }
            else
            {
                currentTextsImageReference.color = Constants.GrayColor;
            }

        }

        for (int i = 0; i < 5; i++)
        {
            Image currentTextsImageReference = textItems[i].gameObject.GetComponentInParent<Image>();

            int actualCount = 0;
            int countInEnteredString = 0;
            int markedGreenCount = 0;

            for (int j = 0; j <i; j++)
            {
                var currentColor = textItems[j].gameObject.GetComponentInParent<Image>().color;

                if (textItems[j].text.ToUpper().Equals(textItems[i].text.ToUpper()) &&
                    !currentColor.Equals(Constants.GreenColor))
                {
                    countInEnteredString++;                
                }
            }

            for (int j = 0; j < 5; j++)
            {
                var currentColor = textItems[j].gameObject.GetComponentInParent<Image>().color;
                var greenColor = Constants.GreenColor;
                if (textItems[i].text.ToUpper().Equals(textItems[j].text.ToUpper()) && currentColor.Equals(greenColor) )
                {
                    markedGreenCount++;
                    Debug.Log(currentColor +" "+ j);
                    //(int)(currentColor.r * 1000) == (int)(greenColor.r * 1000) && (int)(currentColor.g * 1000) == (int)(greenColor.g * 1000) && (int)(currentColor.b * 1000) == (int)(greenColor.b * 1000)
                    
                }
                if (solutionString[j].ToString().ToUpper().Equals(textItems[i].text.ToUpper()))
                {
                    actualCount++;
                }
            }

            if ((markedGreenCount + countInEnteredString) < actualCount && solutionString.ToUpper().Contains(textItems[i].text.ToUpper()) && !currentTextsImageReference.color.Equals(
                    Constants.GreenColor))
            {
                currentTextsImageReference.color = Constants.YellowColor;
                textItems[i].color = Constants.WhiteColor;

            } 
            Debug.Log(markedGreenCount +" "+ countInEnteredString+" "+actualCount + " "+i);
        }

    }
}
