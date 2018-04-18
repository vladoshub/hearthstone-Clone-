using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace Карты
{
    class CheckOnServerJsonFile
    {

        static void Main2(string[] args)
        {
            var obj1 = Deserialize();
            Console.WriteLine(obj1.Cards[2].Hp);
            Console.ReadKey();
            

        }







        public static RootObject  Deserialize()
        {

           
            return JsonConvert.DeserializeObject<RootObject>(File.ReadAllText("json/1.json"));

        }
    }
}
