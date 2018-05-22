using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public struct Players
{


    public string Name;
    public Sprite Logo;
    public int HP,Mana;


    public Players(string name,int hp,int mana)
    {
        Name = name;
        Logo = Resources.Load<Sprite>("Resources/Image/You");
        HP = hp;
        Mana = mana;
    }


   

}

public static class Play
{
    public static Players play = new Players();
}
public class Enemy : MonoBehaviour
{
    public void Awake()
    {
        Play.play.HP = int.Parse(Client.YourHP);
        Play.play.Mana = int.Parse(Client.YourMana);
        Play.play.Name = Client.YourName;


    }
}
