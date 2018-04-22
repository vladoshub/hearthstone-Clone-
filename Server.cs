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

namespace Карты
{
   
    class Server
    {
        public static void func(object param)
        {
            
            NetworkStream stream = param as NetworkStream;
            Console.WriteLine("Connected!");
            Byte[] bytes = new Byte[256];
            String data = null;
            data = null;
            int i;
            int result = 0;



            
            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0 && stream != null && System.Text.Encoding.ASCII.GetString(bytes, 0, i)!="$")
            {

                data += System.Text.Encoding.ASCII.GetString(bytes, 0, i);

            }

            JObject json = JObject.Parse(data);
            string s = JsonConvert.SerializeObject(json);
            var k = JsonConvert.DeserializeObject<RootObject>(s);
            



            if (int.TryParse(data, out result))
            {
                Console.WriteLine("Received: {0}", data);

           
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                System.Threading.Thread.Sleep(5000);
                stream.Write(msg, 0, msg.Length);
                Console.WriteLine("Sent: {0}", data);
            }
            else
            {
                Console.WriteLine("error");
            }


            stream.Close();
        }



        static void Main(string[] args)
        {
            TcpListener server = null;
            try
            {

                Int32 port = 13000;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(localAddr, port);
                server.Start();

                while (true)
                {
                    Console.Write("Waiting for a connection... ");



                    TcpClient client = server.AcceptTcpClient();
                    if (client != null)
                    {


                        NetworkStream stream = client.GetStream();
                        Thread thread;
                        thread = new Thread(func);
                        thread.Start(stream);
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