
using UnityEngine;

public class ChanceScript : MonoBehaviour
{
    [SerializeField] private GameScreenTextBoxScript[] textItems = new GameScreenTextBoxScript[5];

    public void SetText(string enteredAlphabet,int index)
    {
        textItems[index].text.text = enteredAlphabet;
    }

    public string GetString()
    {
        string enteredString = "";
        for (int i = 0; i < 5; i++)
        {
            enteredString += textItems[i].text.text;
        }

        return enteredString;
    }

    public void ColorTheString(string solutionString)
    {
        solutionString = solutionString.ToUpper();
        for (int i = 0; i < 5; i++)
        {
            if (textItems[i].text.text[0].Equals( solutionString[i]))
            {
                textItems[i].image.color = Constants.GreenColor;
                textItems[i].text.color = Constants.WhiteColor;
                textItems[i].state = States.CorrectlyPositionedText;
                //textItems[i].text

            }
            else if (!solutionString.Contains(textItems[i].text.text))
            {
                textItems[i].image.color = Constants.RedColor;
                textItems[i].text.color = Constants.WhiteColor;
                textItems[i].state = States.NotExistingText;
                

                //disable this key

                gameObject.gameObject.GetComponentInParent<GameScreenScript>().DisableKeyBoardKey(textItems[i].text.text);
            }
            else
            {
                textItems[i].image.color = Constants.GrayColor;
                textItems[i].text.color = Constants.WhiteColor;
                textItems[i].state = States.None;
            }

        }

        for (int i = 0; i < 5; i++)
        {
           // Image currentTextsImageReference = textItems[i].image;

            int actualCount = 0;
            int countInEnteredString = 0;
            int markedGreenCount = 0;

            for (int j = 0; j <i; j++)
            {
                var currentColor = textItems[j].image.color;

                if (textItems[j].text.text.ToUpper().Equals(textItems[i].text.text.ToUpper()) &&
                    !textItems[j].state.Equals(States.CorrectlyPositionedText))
                {
                    countInEnteredString++;                
                }
            }

            for (int j = 0; j < 5; j++)
            {
                var currentColor = textItems[j].image.color;
                var greenColor = Constants.GreenColor;
                
                if (textItems[i].text.text.ToUpper().Equals(textItems[j].text.text.ToUpper()) && textItems[j].state.Equals(States.CorrectlyPositionedText))
                {   
                    markedGreenCount++;
                   
                }
                if (solutionString[j].ToString().ToUpper().Equals(textItems[i].text.text.ToUpper()))
                {
                    actualCount++;
                }
            }

            if ((markedGreenCount + countInEnteredString) < actualCount && solutionString.ToUpper().Contains(textItems[i].text.text.ToUpper()) 
                                                                        && !textItems[i].state.Equals(States.CorrectlyPositionedText))
            {
                textItems[i].image.color = Constants.YellowColor;
                textItems[i].text.color = Constants.WhiteColor;
                textItems[i].state = States.CorrectEnteredButWrongPositionText;

            } 
          
        }

    }
}
