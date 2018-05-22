using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
namespace Карты
{

    class Server
    {

        protected static List<NetworkStream> Users = new List<NetworkStream>();
        protected static int count = -1;//нумерация клиентов с 0
        public static int Next=-1;//нумерация след клиентов с 0



        public static void dialog(NetworkStream streamenemy,string dat)
        {
            string data = null;
                    data = data = data.Remove(0, 1);
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                    Thread.Sleep(5000);
                    streamenemy.Write(msg, 0, msg.Length);
                    Thread.Sleep(10);
                

            }
        


        public  void json(NetworkStream streamyou,string dat)
        {
            
            int check_enemy = count % 2;
            Random rand = new Random();
            string data = dat;
                data = data.Remove(0, 1);
                var k = JsonConvert.DeserializeObject<RootObject>(data);//конвертируем строку в объект
                int j = 0;
                int Count = 0;
                while (j != k.Cards.Length)
                {
                    if (k.Cards[j].Type == 2 && (k.Cards[j].Hp != 0 || k.Cards[j].Damage > 7 || k.Cards[j].Damage < 0 || k.Cards[j].Mana > 7 || k.Cards[j].Mana < 0))
                        k.Cards[j] = null;
                    else

                    if (k.Cards[j].Type == 1 && (k.Cards[j].Hp <= 0 || k.Cards[j].Hp > 10 || k.Cards[j].Damage > 7 || k.Cards[j].Damage < 0 || k.Cards[j].Mana > 7 || k.Cards[j].Mana < 0)
                            && ((k.Cards[j].Mana * 2) + 2 < k.Cards[j].Damage + k.Cards[j].Hp))

                        k.Cards[j] = null;
                    else
                        Count++;




                    j++;


                }

                if (Count != 20)
                {
                    data = "error of cardlist";
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                }
                else
                {

                    Card[] c = new Card[20];
                    for (int m = 0; m < 19; m++)
                    {
                        if (k.Cards[m] != null)
                        {
                            c[m] = k.Cards[m];
                        }
                    }
                    RootObject r = new RootObject(k.UserId, c);

                    data = JsonConvert.SerializeObject(r);//конвертируем объект json в строку
                    byte[] msg1 = System.Text.Encoding.ASCII.GetBytes(data);
                    streamyou.Write(msg1, 0, msg1.Length);
                    Thread.Sleep(10);
                    int g = (rand.Next(6, 1000) % 2);
                    msg1 = System.Text.Encoding.ASCII.GetBytes("@" + g.ToString());

                    if (check_enemy == 0)
                    {
                        streamyou.Write(msg1, 0, msg1.Length);
                    }
                    else
                    {
                        if (g == 0)
                        {
                        msg1 = System.Text.Encoding.ASCII.GetBytes("@1");//ходит 2
                            streamyou.Write(msg1, 0, msg1.Length);
                        }
                        if (g == 1)
                        {
                            msg1 = System.Text.Encoding.ASCII.GetBytes("@0");//ходит 1
                            streamyou.Write(msg1, 0, msg1.Length);
                        }

                    }
                    Thread.Sleep(10);

                }
      


        }

        public static void funcJson(object param)
        {
            int check_enemy = count % 2;
            int You = count;
            int i;
            int NextPlayer = Convert.ToInt32(Thread.CurrentThread.Name);
           

                
                if (NextPlayer % 2 != 0)
                {
                    NextPlayer--;
                }
                else
                {

                while (count < NextPlayer)//если четные ждем следующего;
                {

                }
                NextPlayer++;
                }
           
            try
            {
                NetworkStream streamenemy = Users[NextPlayer];//добавляем следующего в список
                NetworkStream streamyou = Users[You];
                while (streamyou != null)
                {
                Byte[] bytes = new Byte[1500];
                string data="";
                while ((i = streamyou.Read(bytes, 0, bytes.Length)) != 0)
                {
                    Thread.Sleep(10);
                    data += System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    if (bytes[i] == 0)
                        break;

                }

                Server S = new Server();
                if(data[0]=='$')//обработка колоды и ходов
                {
                    S.json(streamyou,data);
                }
                if(data[0]=='#')//отправление игровых данных 1игрока второму
                dialog(streamyou,data);

            }
            }
            catch
            {
                try
                {
                    Users[You].Close();
                    Users[NextPlayer].Close();//рвем соедениение  с 2 челами не нарушая списка клиентов
                }
                catch
                {

                }
            }
        }


   








        static void Main(string[] args)
        {
            TcpListener server = null;
            string Server = "";
            Console.WriteLine("введите server");
            Server=Console.ReadLine();
            if (Server == "")
                Server = "127.0.0.1";

            try
            {

                Int32 port = 13000;
                IPAddress localAddr = IPAddress.Parse(Server);
                server = new TcpListener(localAddr, port);
                server.Start();

                while (true)
                {
                    Console.Write("Waiting for a connection... ");



                    TcpClient client = server.AcceptTcpClient();
                    if (client != null)
                    {
                        Thread.Sleep(10);//чтобы следующий клиент не получил неверный Count(зажержка для добавления игрока в список)
                        count++;
                        NetworkStream stream = client.GetStream();
                        Users.Add(stream);
                        Thread thread;
                        thread = new Thread(funcJson);
                        thread.Name = Convert.ToString(count);
                        thread.Start(Users);
                    }
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {

                server.Stop();
            }


            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }

    }
}
