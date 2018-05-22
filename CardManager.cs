using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;


public struct CardMan
{


    public string Name;
    public Sprite Logo;
    public int Attack, HP,Type,Mana;


    public CardMan(string name, string logo, int attack, int hp,int type,int mana)
    {
        Name = name;
        Logo = Resources.Load<Sprite>(logo);
        Attack = attack;
        HP = hp;
        Type = type;
        Mana = mana;


    }

    
}

public static class CardList
{
    public static List<CardMan> AllCards = new List<CardMan>();
}
public class CardManager:MonoBehaviour
{
   
    
    public void Awake()
    {
       
        
        Client client = new Client();
        var k = JsonConvert.DeserializeObject<RootObject>(Client.YourColoda);
        for (int i = 0; i < 25; i++)
        {
        
            CardList.AllCards.Add(new CardMan(k.Cards[i].Name,"Resources/Image/"+i.ToString()+".jpeg".ToString(), k.Cards[i].Damage, k.Cards[i].Hp,k.Cards[i].Type, k.Cards[i].Mana));
        }
    }
}




	

