using UnityEngine;

public class Constants 
{
  public static Color GreenColor=new Color(0,1,0,1);
  public static Color GrayColor=new Color(0.5f, 0.5f, 0.5f, 1);
  public static Color WhiteColor=new Color(1,1,1,1);
  public static Color YellowColor = new Color(1, 0.92f, 0.016f, 1);
  public static Color RedColor = new Color(1, 0, 0, 1);
  public static string SaveFile = Application.persistentDataPath + "/gamedata.json";
  public static string[] keyBoardKeys = new string[26]
  {
    "Q","W","E","R","T","Y","U","I","O","P","A","S","D","F","G","H","J","K","L","Z","X","C","V","B","N","M"
  };

}
