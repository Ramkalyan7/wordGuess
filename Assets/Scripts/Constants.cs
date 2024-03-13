using UnityEngine;

public class Constants 
{
  public static Color GREENCOLOR=new Color(0,1,0,1);
  public static Color GRAYCOLOR=new Color(0.5f, 0.5f, 0.5f, 1);
  public static Color WHITECOLOR=new Color(1,1,1,1);
  public static Color YELLOWCOLOR = new Color(1, 0.92f, 0.016f, 1);
  public static Color REDCOLOR = new Color(1, 0, 0, 1);
  public static Color SKYBLUECOLOR= new Color(0.53f,0.8f,0.92f,1);
  public static string SAVEFILE = Application.persistentDataPath + "/gamedata.json";
  public static string GAMEOVERWINWISHTEXT = "Congrats You won ! Word is ";
  public static string GAMEOVERLOSTWISHTEXT =  "Sorry , You Lost !";
  
  public static string[] keyBoardKeys = new string[26]
  {
    "Q","W","E","R","T","Y","U","I","O","P","A","S","D","F","G","H","J","K","L","Z","X","C","V","B","N","M"
  };

}
