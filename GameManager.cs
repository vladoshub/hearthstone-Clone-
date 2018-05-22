using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Threading;


public class Game
{
    public List<CardMan> EnemyDeck, PlayerDeck,
                         EnemyHand, PlayerHand,
                         EnemyField, PlayerField;
    public Players
                           PlayerFace;

    public Enemys EnemyFace;


    public Game()
    {
        //EnemyDeck = GiveEnemyCard(Client.Enemy);
        PlayerDeck = GiveDeckCard();

        EnemyHand = new List<CardMan>();
        PlayerHand = new List<CardMan>();

        EnemyField = new List<CardMan>();
        PlayerField = new List<CardMan>();

        EnemyFace = new Enemys();
        PlayerFace = new Players();



    }

    List<CardMan> GiveDeckCard()//жать колоду карт из клиента
    {
        List<CardMan> list = new List<CardMan>();
        for (int i = 0; i < 25; i++)
            list.Add(CardList.AllCards[i]);
        return list;
    }


    List<CardMan> GiveEnemyCard(string s)
    {  
        var k = JsonConvert.DeserializeObject<Player>(s);
        Client.EnemyHp = k.HPPlayer;
        Client.EnemyMana = k.ManaPlayer;
        List<CardMan> list = new List<CardMan>();
        
        CardMan C = new CardMan(k.name, k.name, int.Parse(k.DAMAGE), int.Parse(k.HP), int.Parse(k.TYPE), int.Parse(k.MANA));
        list.Add(C);
        return list;
    }

}



public class GameManager : MonoBehaviour {

    public Game CurrentGame;

    public Transform EnemyHand, PlayerHand, PlayerFace, EnemyFild, EnemyFace;
    public GameObject CardPref;
    public GameObject EnemyCard;
    public GameObject YouFace;
    public GameObject EnFace;
    //public List<GameObject> Objects;
    public static int Turn, TurnTime = 6;
    public TextMeshProUGUI TurnTimeText;
    public Button EndTurnBtn;
    public static bool isPlay;
   


    //public  static void Play(bool s)
    //{
    //    if(s)
    //   ChangeTurn();


    //}
    //public int CountCards = 0;


    public bool IsPlayerTurn
    {
        get
        {
            isPlay = Turn % 2 == 0;
            return Turn % 2 == 0;

        }
    }

    void Start()
    {

        Client.YourHP = "30";
        Client.YourMana = "30";
        if (Client.ready == false)
        {
            Turn = 1;
            EndTurnBtn.interactable = false;
        }
        else
        {
            Turn = 0;

        }

        CurrentGame = new Game();
        GiveHandCards(CurrentGame.PlayerDeck, PlayerHand);//наши карты
                                                          //GiveYouFace(CurrentGame.PlayerFace,PlayerFace);наше лицо
                                                          //GiveEnFace(CurrentGame.EnemyFace, EnemyFace);
                                                          //Thread thread;
                                                          /* thread = new Thread(Client.GetCardsFromEnemy);*///запустить поток прослушки данных от сервера(ходы врага)
        StartCoroutine(TurnFunc());

    }











    void GiveHandCards(List<CardMan> deck, Transform hand)//дать колоду на игровое поле(4 карты)
    {
        int i = 0;
        while (i++ < 7)
            GiveCardToHand(deck, hand);

    }

    void GiveEnemyCards(List<CardMan> deck, Transform hand)
    {
        if (Client.Enemy == "")//обработка вариантов атаки
            GiveCardToHand(deck, hand);

    }

    void GiveYouFace(Players face, Transform PlayerFace)
    {
        GameObject PlFace = Instantiate(YouFace, PlayerFace, false);
        PlFace.GetComponent<YouInterface>().ShowPlayerInfo(face);

        //PlFace.tag = CountCards.ToString();
        //Objects.Add(PlFace);
        //CountCards++;

    }
    void GiveEnFace(Enemys face, Transform PlayerFace)
    {
        GameObject enFace = Instantiate(EnFace, EnemyFace, false);
        enFace.GetComponent<EnemyInterface>().ShowEnemyInfo(face);

    }

    void GiveEnemyCardToFiled(List<CardMan> deck, Transform hand) //указать EnemyDeck и вражескрое поле
    {
        if (deck.Count == 0)
            return;
        CardMan card = deck[0];
        GameObject cardGo = Instantiate(EnemyCard, hand, false);
        cardGo.GetComponent<CardInterface>().ShowCardInfo(card);
        deck.RemoveAt(0);


    }

    void GiveCardToHand(List<CardMan> deck, Transform hand)
    {
        if (deck.Count == 0)
            return;
        CardMan card = deck[0];
        GameObject cardGo = Instantiate(CardPref, hand, false);
        if (hand == EnemyHand)
            cardGo.GetComponent<CardInterface>().HideCardInfo(card);
        else
            cardGo.GetComponent<CardInterface>().ShowCardInfo(card);
        cardGo.tag = "Finish";
        cardGo.name = card.Name;
        deck.RemoveAt(0);


    }

    IEnumerator TurnFunc()
    {

        TurnTime = 6;
        TurnTimeText.text = TurnTime.ToString();
        if (IsPlayerTurn)
        {
            while (TurnTime-- > 0)
            {
                TurnTimeText.text = TurnTime.ToString();
                yield return new WaitForSeconds(1);
            }
            nul();
            GiveNewCard();//дать 1 карту

            //GiveEnFace(CurrentGame.EnemyFace, EnemyFace);
            //Client.UpLoadOnServer(Client.stream, Client.client, 0, "NO");
            ChangeTurn();

        }

        else
        {
            while (TurnTime-- > 0) /* && Client.GetsData!=true -пока не получим данные от серва или счетчик не равен 0*/
            {
                TurnTimeText.text = TurnTime.ToString();
                yield return new WaitForSeconds(1);
            }
            //Client.GetsData = false;
            //if(Client.Enemy!="NO")
            var k = JsonConvert.DeserializeObject<Player>(Client.Enemy);
            if (k.NUM == "field")
            {
                //GiveEnemyCards(CurrentGame.EnemyDeck, EnemyFild);
            }
            if (k.NUM == "face")
            {
                //GiveYouFace(CurrentGame.EnemyFace, EnemyFace);
            }
            else
            {

                List<CardMan> list = new List<CardMan>();
                int j = 0;
                while (list[j].Name != k.NUM)
                    j++;
                if (GameObject.Find(k.NUM) != null)
                {
                    GameObject g = GameObject.Find(k.NUM);
                }
                if (GameObject.Find("0" + k.NUM) != null)
                {
                    GameObject g = GameObject.Find("0" + k.NUM);
                    CardInterface CardMe = g.GetComponent<CardInterface>();
                }





            }
            ChangeTurn();
        }

    }

    

    public   void ChangeTurn()
    {
        StopAllCoroutines();
        Turn++;
        EndTurnBtn.interactable = IsPlayerTurn;
        if (IsPlayerTurn&&DropPlace.s!="")//если наш ход отправляем ход
        {
            Client.UpLoadOnServer(Client.stream, Client.client, DropPlace.C, DropPlace.s);
            DropPlace.s = "";
            Thread.Sleep(1);//подождем пока ответ дойдет и обработается до 2 клиента
        }
        if (IsPlayerTurn && DropPlace.s == "")
        {
            Client.UpLoadOnServer2();
            DropPlace.s = "";
            Thread.Sleep(1);
        }
        if (!IsPlayerTurn)//если наш ход-добавить 1 карту по нажатию на кнопку
            GiveNewCard();
        StartCoroutine(TurnFunc());

    }

    void GiveNewCard()
    {
        //GiveCardToHand(CurrentGame.EnemyDeck, EnemyHand);
        if (7 > GameObject.FindGameObjectsWithTag("Finish").Length) //проверка на то чтобы на поле было не более 7 карт
        GiveCardToHand(CurrentGame.PlayerDeck, PlayerHand);
    }
    public void GameLogicface(CardInterface you, Enemys face)
    {
       Client.EnemyHp = (int.Parse(Client.EnemyHp) - you.SelfCard.Attack).ToString();
        face.HP = int.Parse(Client.EnemyHp);
        you.SelfCard.HP = 0;

        //Client.UpLoadOnServer(Client.stream, Client.client, 0, "NO");

    }
    public void GameLogiccard(CardInterface you, CardInterface en)
    {
        Client.YourMana = (int.Parse(Client.YourMana) - you.SelfCard.Mana).ToString();
        you.SelfCard.HP=you.SelfCard.HP - en.SelfCard.HP;
        en.SelfCard.HP = en.SelfCard.HP - you.SelfCard.Attack;
        //Client.UpLoadOnServer(Client.stream, Client.client, 0, "NO");

    }

    public void nul ()
        {
        
        while (DropPlace.CardOnField.Count > 0)
        {
            DropPlace.CardOnField[0].name.Remove(DropPlace.CardOnField[0].name.Length-1);//добавляем единицу для того чтобы карту можно было двигать с поля в след ходе
            DropPlace.CardOnField.RemoveAt(0);
        }

    }

}
