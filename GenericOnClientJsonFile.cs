using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace Карты
{
    class GenericOnClientJsonFile
    {

        
        static void Main(string[] args)
        {


           string name;
           int Hp, Damage, Mana, Type;

            Card[] C2 = new Card[2];
            for (int j = 0; j < 2; j++)
            {
                Console.WriteLine("Name");
            name = Console.ReadLine();
            Console.WriteLine("hp");
            Hp = int.Parse(Console.ReadLine());
            Console.WriteLine("Damage");
            Damage = int.Parse(Console.ReadLine());
            Console.WriteLine("Mana");
            Mana = int.Parse(Console.ReadLine());
            Console.WriteLine("Type");
            Type = int.Parse(Console.ReadLine());
            Console.WriteLine("hp");

                Card C = new Card(Hp,Mana,Type,name,Damage);
                C2[j] = C;

            }
            RootObject R = new RootObject("OnePerson", C2);
            Serialize(R);
        }


        public static void Serialize(RootObject r)
        {

            
            
            File.WriteAllText(@"D:\Kaloda.json", JsonConvert.SerializeObject(r));

            
            using (StreamWriter file = File.CreateText(@"D:\Kaloda.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, r);
            }


        }

    }
}
