using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardInterface : MonoBehaviour
{
    public CardMan SelfCard;
    public Image Logo;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Mana;
    public TextMeshProUGUI Type;
    public TextMeshProUGUI Damage;
    public TextMeshProUGUI Hp;
    public void HideCardInfo(CardMan card)
    {
        //SelfCard = card;
        //Logo.sprite = null;
        //Name.text = "";
        ShowCardInfo(card);

    }
    public void ShowCardInfo(CardMan card)
    {
        Hp.text = card.HP.ToString();
        Mana.text = card.Mana.ToString();
        Damage.text = card.Attack.ToString();
        Type.text = card.Type.ToString();
        SelfCard = card;
        Logo.sprite = card.Logo;
        Logo.preserveAspect = true;
        Name.text = card.Name;
    }
    private void Start()
    {
       // ShowCardInfo(CardList.AllCards[transform.GetSiblingIndex()]); 
    }



}
