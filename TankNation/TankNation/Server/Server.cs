using System;
using System.Collections.Generic;
using Lidgren.Network;
using System.Threading;

namespace Server
{
    class Server
    {
        NetServer server;
        List<NetPeer> clients;
        List<Tank> tanks;
        int id_tracker = 0;

        public Server()
        {
            tanks = new List<Tank>();
            StartServer();
            Thread serverThread = new Thread(RecieveMessages);
            serverThread.IsBackground = true;
            serverThread.Start();
        }

        void StartServer()
        {
            var config = new NetPeerConfiguration("TankNation") { Port = 14242 };
            server = new NetServer(config);
            server.Start();

            if (server.Status == NetPeerStatus.Running)
            {
                Console.WriteLine("Server is running on port " + config.Port);
            }
            else
            {
                Console.WriteLine("Server not started...");
            }
            clients = new List<NetPeer>();
        }

        void RecieveMessages()
        {
            NetIncomingMessage message;
            var stop = false;

            while (!stop)
            {
                while ((message = server.ReadMessage()) != null)
                {
                    switch (message.MessageType)
                    {
                        case NetIncomingMessageType.Data:
                            {
                                var data = message.ReadString();
                                Console.WriteLine(data);

                                if (data == "exit")
                                {
                                    stop = true;
                                }

                                // Send response
                                String[] updates = data.Split(':');
                                if (updates.Length == 5)
                                    UpdateTank(updates);

                                break;
                            }
                        case NetIncomingMessageType.DebugMessage:
                            Console.WriteLine(message.ReadString());
                            break;
                        case NetIncomingMessageType.StatusChanged:
                            Console.WriteLine(message.SenderConnection.Status);
                            if (message.SenderConnection.Status == NetConnectionStatus.Connected)
                            {
                                clients.Add(message.SenderConnection.Peer);
                                Console.WriteLine("{0} has connected.", message.SenderConnection.Peer.Configuration.LocalAddress);
                                

                            }
                            if (message.SenderConnection.Status == NetConnectionStatus.Disconnected)
                            {
                                clients.Remove(message.SenderConnection.Peer);
                                Console.WriteLine("{0} has disconnected.", message.SenderConnection.Peer.Configuration.LocalAddress);
                            }
                            break;
                        default:
                            Console.WriteLine("Unhandled message type: {message.MessageType}");
                            break;
                    }
                    server.Recycle(message);
                }
            }

            Console.WriteLine("Shutdown package \"exit\" received. Press any key to finish shutdown");
            Console.ReadKey();
        }

        void SendMessage(string message)
        {
            for(int i = 0; i < server.Connections.Count; i++)
            {
                NetOutgoingMessage sendMsg = server.CreateMessage();
                sendMsg.Write(message);
                
                server.SendMessage(sendMsg, server.Connections[i], NetDeliveryMethod.ReliableOrdered); //what the hell is recipient??? idk if this works
            }
        }

        void UpdateTank(String[] updateString)
        {

            // "1:76:50:16"
            // "ID:HP:POS.X:POS.Y" etc.
            int ID = Convert.ToInt32(updateString[0]);

            foreach( Tank tank in tanks)
            {
                if(tank.ID == ID)
                {
                    tank.health = Convert.ToInt32(updateString[1]);
                    tank.positionX = Convert.ToInt32(updateString[2]);
                    tank.positionY = Convert.ToInt32(updateString[3]);
                    tank.kills = Convert.ToInt32(updateString[4]);
                    tank.angle = Convert.ToInt32(updateString[5]);
                }
                string tankUpdate = $"{tank.ID}:{tank.health}:{tank.positionX}:{tank.positionY}:{tank.kills}:{tank.angle}";
                SendMessage(tankUpdate);
            }
        }

    }
}
