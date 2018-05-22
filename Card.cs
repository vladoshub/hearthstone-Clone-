using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;


    public partial class RootObject : MonoBehaviour
    {

        [JsonProperty("UserId")]
        public string UserId { get; set; }



        [JsonProperty("Cards")]
        public Card[] Cards { get; set; }

        public RootObject(string name, Card[] Tx)
        {
            UserId = name;
            Cards = Tx;

        }
    }

    public partial class Card:MonoBehaviour
    {
        public Card(int hp, int mana, int type, string name, int damage)
        {
            Hp = hp;
            Mana = mana;
            Type = type;
            Name = name;
            Damage = damage;

        }


        [JsonProperty("hp")]
        public int Hp { get; set; }

        [JsonProperty("mana")]
        public int Mana { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("damage")]
        public int Damage { get; set; }
    }

