using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;



public struct Enemys
{

    public string Name;
    public Sprite Logo;
    public int HP, Mana;


    public Enemys(string name, int hp, int mana)
    {
        Name = name;
        Logo = Resources.Load<Sprite>("Resources/Image/Enemy");
        HP = hp;
        Mana = mana;
    }

}


public static class Enemyget
{
    public static Enemys enemy = new Enemys();
}
public class EnemyAwake : MonoBehaviour
{
    public void Awake()
    {
       
        var k = JsonConvert.DeserializeObject<Player>(Client.Enemy);
        Enemyget.enemy.HP = int.Parse(k.HPPlayer);
        Enemyget.enemy.Mana = int.Parse(k.ManaPlayer);
        Enemyget.enemy.Name = k.UserId;


    }
}
