using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class Player : MonoBehaviour
{


    [JsonProperty("UserId")]
    public string UserId { get; set; }

    [JsonProperty("ManaPlayer")]
    public string ManaPlayer { get; set; }
    [JsonProperty("HPPlayer")]
    public string HPPlayer { get; set; }




    [JsonProperty("HP")]
    public string HP { get; set; }
    [JsonProperty("MANA")]
    public string MANA { get; set; }
    [JsonProperty("TYPE")]
    public string TYPE { get; set; }
    [JsonProperty("DAMAGE")]
    public string DAMAGE { get; set; }
    [JsonProperty("NUM")]
    public string NUM { get; set; }

    public Player(string name, string mana, string hp, string Hp, string Type, string Damage, string Mana,string num)
    {
        UserId = name;
        HPPlayer = hp;
        ManaPlayer = mana;
        HP = Hp;
        MANA = Mana;
        TYPE = Type;
        DAMAGE = Damage;
        NUM = NUM;


    }
}
