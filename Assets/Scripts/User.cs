using System;
using System.Collections.Generic;


[Serializable]
public class  Chance
{
    public int ChanceNumber;
    public string StringEntered;

    public Chance()
    {
        ChanceNumber = 0;
        StringEntered = "";
        
    }
}


[Serializable]
public class User 
{ 
    public string UserName;

    public int CurrentLevel = 1;

    public int CurrentPlayingLevel=1;
    //different states are notattempted , incomplete and failed.
    //public string StateOfTheLevel;
     public List<Chance> Chances;
     
     //single ton Instance
     private static User _instance;
     public static User Instance {
         get
         {
             if (_instance == null)
             {
                 _instance = new User();
             }

             return _instance;
         }

         set
         {
             _instance = value;
         }
         
     }

    
     

}


