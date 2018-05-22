using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class YouInterface : MonoBehaviour
{

    public Players SelfCard;
    public Image Logo;
    public TextMeshProUGUI Name,Mana,Hp;
  
    public void ShowPlayerInfo(Players You)
    {
        SelfCard = You;
        Logo.sprite = You.Logo;
        Logo.preserveAspect = true;
        Name.text = You.Name;
        Mana.text = You.Mana.ToString();
        Hp.text = You.Mana.ToString();
    }
    private void Start()
    {
        ShowPlayerInfo(Play.play);
    }
}
