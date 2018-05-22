using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Threading.Tasks;
using System.Threading;



public enum FieldType
{
    SELF_FACE,
    SELF_CARD,
    SELF_HAND,
    SELF_FIELD,
    ENEMY_HAND,
    ENEMY_FIELD,
    ENEMY_FACE,
    ENEMY_CARD,

    

}




public class DropPlace : MonoBehaviour, IDropHandler,IPointerExitHandler,IPointerEnterHandler
{
    public FieldType Type;
    public static List<GameObject> CardOnField;
    public static CardMan C;//-какой картой бьем
    public static string s;//по кому 
    static GameManager g;


    public void Start()
    {
        s = "";
        CardMan C = new CardMan();
        CardOnField = new List<GameObject>();
        g = new GameManager();

    }


    public void OnDrop(PointerEventData eventData)
    {
        GameManager Man = new GameManager();

        if (GameManager.isPlay)
        {

            if (Type == FieldType.ENEMY_CARD) //если атаковали лицо противника карту
            {
                if (eventData.pointerDrag.gameObject.name == "0")
                    return;
                GameObject CardUnderMouse = eventData.pointerCurrentRaycast.gameObject;//что под курсором
                GameObject CardInMouse = eventData.pointerDrag.gameObject;// что дропнули
                CardInMouse.tag = "Respawn";
                CardInterface CardMe = CardInMouse.GetComponent<CardInterface>();
                CardInterface CardEn = CardInMouse.GetComponent<CardInterface>();
                Man.GameLogiccard(CardMe, CardEn);
                if (CardMe.SelfCard.HP <= 0)
                    Destroy(CardInMouse);
                if (CardEn.SelfCard.HP <= 0)
                {
                        Destroy(CardUnderMouse);
                    }

                C = CardMe.SelfCard;
                s = CardEn.SelfCard.Name;
                CardEn.SelfCard.Attack = 5;


            }

            if (Type == FieldType.ENEMY_FACE)//если атаковали лицо противника
            {
                if (eventData.pointerDrag.gameObject.name == "0")
                    return;
                GameObject Face = eventData.pointerCurrentRaycast.gameObject;
                GameObject CardInMouse = eventData.pointerDrag.gameObject;
                CardInMouse.tag = "Respawn";
                CardInterface CardMe = CardInMouse.GetComponent<CardInterface>();
                Enemys FaceEn = CardInMouse.GetComponent<Enemys>();

                Man.GameLogicface (CardMe, FaceEn);
                if (FaceEn.HP <= 0)
                {

                    Destroy(Face);
                    //GameOver;
                }
                C = CardMe.SelfCard;
                s = "Face";

            }




            if (Type != FieldType.SELF_FIELD)//если поместили картку на поле
                return;
            else
            {
                if (eventData.pointerDrag.gameObject != null)
                {
                    eventData.pointerDrag.gameObject.name = eventData.pointerDrag.gameObject.name + "0";
                    eventData.pointerDrag.gameObject.tag = "Respawn";
                    CardOnField.Add(eventData.pointerDrag.gameObject);
                    CardInterface CardMe = g.GetComponent<CardInterface>();
                    C = CardMe.SelfCard;
                    s = "Fields";
                }
    
            }
            CardMoving card = eventData.pointerDrag.GetComponent<CardMoving>();
            if (card)
                card.DefautlParent = transform;
            GameManager.TurnTime = 0;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GameManager.isPlay)
        {
            if (eventData.pointerDrag == null || Type == FieldType.ENEMY_FIELD || Type == FieldType.ENEMY_HAND)
                return;
            CardMoving card = eventData.pointerDrag.GetComponent<CardMoving>();
            if (card)
                card.DefaultTempCardParent = transform;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;
        CardMoving card = eventData.pointerDrag.GetComponent<CardMoving>();
        if (card&&card.DefaultTempCardParent == transform)
            card.DefaultTempCardParent = card.DefautlParent;
    }
}
