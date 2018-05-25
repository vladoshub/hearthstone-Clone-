using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Threading.Tasks;




class Client:MonoBehaviour
{
        public  static string YourName { get; set; }
        public  static string YourMana { get; set; }
        public static string YourHP { get; set; }
        public static bool GameSession=true;
        public static  string YourColoda { get; set;}
        public static string Enemy { get; set; }//ходы от противника
        public static string EnemyHp{ get; set; }
        public static string EnemyMana { get; set; }
        public static int GameStart=0;
        public static Int32 port = 13000;
        public static TcpClient client { get; set; }
        public static NetworkStream stream { get; set; }
        public static bool ready { get; set; }//флаг хода
        public static bool check_itteration = false;
        public static bool GetsData = false;//проверка получили ли мы данные раньше оконачания счетчика
        public static bool CheckonError=true;//флаг ошибок




    //public static void CheckPlayer(NetworkStream stream)
    //{
    //    string Turn = "";
    //    int i;
    //    try
    //    {


    //        Byte[] bytes = new Byte[6];
    //        while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
    //        {

    //            Turn += System.Text.Encoding.ASCII.GetString(bytes, 0, i);
    //            if (bytes[i] == 0)
    //                break;

    //        }

    //        Turn = Turn.Remove(0, 1);
    //        if (Turn == "0")
    //        {
    //            ready1 = 1;
    //            check_itteration = true;
    //        }
            
    //        if (Turn == "1")
    //            ready1 = 2;

    //    }
    //    catch (Exception e)
    //    {

    //    }

    //}


    public static  void GetCardsFromEnemy()//прослушка сервера
        {
        NetworkStream stream1 = stream;
        string Vrag="";
            int i;
        try
        {
            while (stream1 != null)
            {
                Byte[] bytes = new Byte[1000];
                while ((i = stream1.Read(bytes, 0, bytes.Length)) != 0)
                {
                    Vrag += System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    if (bytes[i] == 0)
                        break;
                }
                if (Vrag == "next")
                {
                    NEXT = true;
                    
                }
                else
                {
                    GetsData = true;//флаг получения данных от сервера
                    Enemy = Vrag;
                }
                Vrag = "";
                Thread.Sleep(10);
            }

            
         
        }
        catch (Exception e)
        {
            CheckonError = false;
        }

        }

    public static void CheckTurn(NetworkStream stream)//поулчение хода от сервера
    {
        string Turn = "";
        int i;
        try
        {


            Byte[] bytes = new Byte[3];
            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            {

                Turn += System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                if (bytes[i] == 0)
                    break;

            }

            Turn = Turn.Remove(0, 1);
            if (Turn == "0")
                ready = true;
            if (Turn == "1")
                ready = false;
  
        }
        catch (Exception e)
        {
            CheckonError = false;
        }

    }


    public static void SaveColoda(NetworkStream stream,TcpClient client)//сохранение колоды которая получена от сервера
        {
        try
        {
            Byte[] data;
            string message = "";
            message = (File.ReadAllText("json/YourColoda.json"));//пишем json в string
            message = "$"+message;
            data = System.Text.Encoding.ASCII.GetBytes(message);
            stream.Write(data, 0, data.Length);
            data = new Byte[10000];
            String responseData = String.Empty;
            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            if (responseData == "error of list card")
                CheckonError = false;
            YourColoda= responseData;
        }
        catch (Exception e)
        {
            CheckonError = false;
        }
        }

    public static void UpLoadOnServer(NetworkStream stream, TcpClient client, CardMan Cards, string s)//загрузка данных на сервер
    {
        try
        {
            Byte[] data;
            string message = "";

            Player P1 = new Player(YourName, YourMana, YourHP, Cards.HP.ToString(), Cards.Type.ToString(), Cards.Attack.ToString(), Cards.Mana.ToString(), s);
            message = JsonConvert.SerializeObject(P1);
            message = "#" + message;
            data = System.Text.Encoding.ASCII.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }
        catch (Exception e)
        {
            CheckonError = false;
        }

    }


         public static void UpLoadOnServer2()//загрузка данных на сервер(если чел не сходил картой)
        {
            try
            {
                Byte[] data;
                string message = "NO";
               
                    message = "#" + message;
                    data = System.Text.Encoding.ASCII.GetBytes(message);
                    stream.Write(data, 0, data.Length);

                
            }
            catch (Exception e)
        {
            CheckonError = false;
        }
           
        }





       public static string  Connect(String server)
        {
            
            try
            {
               
                port = 13000;
                client = new TcpClient(server, port);
                stream = client.GetStream();
            return "ok";
           
            }
          
            catch (Exception e)
            {
            return "error";
        }

        }
    }

