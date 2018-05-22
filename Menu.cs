using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour {

    public int window;

	void Start ()
    {
        window = 1;

	}
    int i = 1;
    string text = "";
    string l = "wait";

    private void OnGUI()
    {
        
        
        GUI.BeginGroup(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 200));
        if (window == 1)
        {
            if (GUI.Button(new Rect(10, 30, 180, 30), "Играть"))
            {
                window = 2;
            }
            if (GUI.Button(new Rect(10, 70, 180, 30), "Настройки"))
            {
                window = 3;
            }
            if (GUI.Button(new Rect(10, 110, 180, 30), "Об Игре"))
            {
                window = 4;
            }
            if (GUI.Button(new Rect(10, 150, 180, 30), "Выход"))
            {
                window = 5;
            }
        }

        if (window == 2)
        {
            GUI.Label(new Rect(50, 10, 180, 30), "Multiplayer");
            if (GUI.Button(new Rect(10, 40, 180, 30), "начать подбор игроков"))
            {
                window =6;
            }
            
            if (GUI.Button(new Rect(10, 160, 180, 30), "Назад"))
            {
                window = 1;
            }
        }
        if (window == 3)
        {
            GUI.Label(new Rect(50, 10, 180, 30), "Настройки Игры");
            if (GUI.Button(new Rect(10, 40, 180, 30), "Игра"))
            {
            }
            if (GUI.Button(new Rect(10, 80, 180, 30), "Аудио"))
            {
            }
            if (GUI.Button(new Rect(10, 120, 180, 30), "Видео"))
            {
            }
            if (GUI.Button(new Rect(10, 160, 180, 30), "Назад"))
            {
                window = 1;
            }
        }
        if (window == 4)
        {
            GUI.Label(new Rect(50, 10, 180, 30), "Об Игре");
            GUI.Label(new Rect(10, 40, 180, 40), "Информация об разработчике и об игре");
            if (GUI.Button(new Rect(10, 90, 180, 30), "Назад"))
            {
                window = 1;
            }
        }
        if (window == 5)
        {
            GUI.Label(new Rect(50, 10, 180, 30), "Вы уже выходите?");
            if (GUI.Button(new Rect(10, 40, 180, 30), "Да"))
            {
                Application.Quit();
            }
            if (GUI.Button(new Rect(10, 80, 180, 30), "Нет"))
            {
                window = 1;
            }
        }
        if (window == 6)
        {
           
            text = GUI.TextField(new Rect(20, 50, 190, 30),text, 25);
            GUI.Label(new Rect(50, 10, 180, 30), "введите ваше имя");
            GUI.Label(new Rect(50, 150, 180, 30), l);
            if (GUI.Button(new Rect(20, 100, 180, 30), "Играть!"))
            {
                //while (Client.ready1==1)
                //{
                //    l = "поиск игрока";
                //    Thread.Sleep(10);
                //    Client.CheckPlayer(Client.stream);
                //}
                
                Client client = new Client();
                Client.YourName = text;
                Client.Connect("127.0.0.1");
                //Client.YourColoda=Client.SaveColoda(Client.stream, Client.client);
                Client.SaveColoda(Client.stream, Client.client);
                Client.CheckTurn(Client.stream);
                if((Client.ready)==true)
                {
                    window = 7;
                }
                else
                {
                    window = 8;
                }

            }
           
        }
        if(window==7)
        {
            
            l = "ваш ход";
            GUI.Label(new Rect(50, 150, 180, 30), l);
            Thread.Sleep(1000);
            while (i > 0)
            {

                i--;
                l = "до игры осталось:" + i.ToString() + "ceк";
                Thread.Sleep(1000);
                
                CardManager card = new CardManager();
                card.Awake();
                SceneManager.LoadScene(1);
            }
        }
        if(window==8)
        {
            l = "ход противника";
            Thread.Sleep(1000);
            while (i > 0)
            {
                i--;
                l = "до игры осталось:" + i.ToString() + "ceк";

                Thread.Sleep(1000);
               
                CardManager card = new CardManager();
                card.Awake();
                SceneManager.LoadScene(1);
            }
        }

            GUI.EndGroup();
    }

    void Update ()
    {
		

	}
}
