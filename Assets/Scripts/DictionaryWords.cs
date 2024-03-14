

using System.Collections.Generic;

public class DictionaryWords
{
   public HashSet<string> dictionary = new HashSet<string>();

   private static DictionaryWords _Instance;

   public static DictionaryWords Instance
   {
      get
      {
         if (_Instance == null)
         {
            _Instance = new DictionaryWords();
         }

         return _Instance;
      }

      set
      {
         _Instance = value;
      }
   }
}
