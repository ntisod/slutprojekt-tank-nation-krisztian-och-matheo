using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using System.Threading;
using Microsoft.Xna.Framework;
//Här vi har adderad using Lidgren som betyder att  det använder lidgren libary och vi va tvungen att adera ett references.
namespace TankNation
{
    class Client
    {
        NetClient client;
        int ID;
        public List<Tank> tanks;

        public Client()
        {
            Startclient();
        }
        void Startclient()
        {
            var config = new NetPeerConfiguration("TankNation"); //här startar vi clienten och koplar till server på localhost och
            //på 14242 port
            config.AutoFlushSendQueue = false;
            client = new NetClient(config);
            client.Start();

            string ip = "localhost";
            int port = 14242;
            client.Connect(ip, port);
            ClientListenThread();
        }
        void ClientListenThread()// Vi anväder Threads här för att lysna
        {
            Thread clientThread = new Thread(ReciveMessage);
            clientThread.IsBackground = true;
            clientThread.Start();

        }
        public void SendMessage(string key)
        {
            NetOutgoingMessage message = client.CreateMessage(key);

            client.SendMessage(message, NetDeliveryMethod.ReliableOrdered);
            client.FlushSendQueue();

        }
        void ReciveMessage()//Här clientent lysnar på message
        {
            while (true)
            {
                NetIncomingMessage msg;
                bool tankadded = false;
                while ((msg = client.ReadMessage()) != null)
                {
                    
                }
            }

        }
        void GetUpdate()
        {


        }
    }
}
