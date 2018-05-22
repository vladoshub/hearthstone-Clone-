using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyInterface : MonoBehaviour
{

    public Enemys SelfCard;
    public Image Logo;
    public TextMeshProUGUI Name,Mana,Hp;

    public void ShowEnemyInfo(Enemys enemy)
    {
        SelfCard = enemy;
        Logo.sprite = enemy.Logo;
        Logo.preserveAspect = true;
        Name.text = enemy.Name;
        Mana.text = enemy.Mana.ToString();
        Hp.text = enemy.Mana.ToString();
    }
    private void Start()
    {

        ShowEnemyInfo(Enemyget.enemy); 
        
    }
}
