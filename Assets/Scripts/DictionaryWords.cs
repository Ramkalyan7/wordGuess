

using System.Collections.Generic;

public class DictionaryWords
{
   //public HashSet<string> dictionary = new HashSet<string>();
   public List<string> dictionary;

   private static DictionaryWords _instance;

   public static DictionaryWords Instance
   {
      get
      {
         if (_instance == null)
         {
            _instance = new DictionaryWords();
         }

         return _instance;
      }

      set
      {
         _instance = value;
      }
   }
}
