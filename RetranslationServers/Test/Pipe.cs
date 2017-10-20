using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RetranslationServers
{
    class Pipe
    {
        TcpListener serverIn;
        TcpClient clientOut;
        int portIn;
        int portOut;
        IPAddress localAdress1;
        string localAdress2;

        public Pipe(string local1, string local2, int port1, int port2)
        {

            localAdress1 = IPAddress.Parse(local1);
            localAdress2 = local2;
            portIn = port1;
            portOut = port2;
        }

        public void Listen()
        {
            serverIn = new TcpListener(localAdress1, portIn);
            clientOut = new TcpClient(localAdress2, portOut);
            serverIn.Start();

            while (true)
            {
                TcpClient clientIn = serverIn.AcceptTcpClient();
                string nameOfClient = ((IPEndPoint)clientIn.Client.LocalEndPoint)+"";
                clientIn.ReceiveBufferSize = 1024;
                NetworkStream streamIn = clientIn.GetStream();
                NetworkStream streamOut = clientOut.GetStream();
                byte[] bytes = new byte[clientIn.ReceiveBufferSize];
                string data;
                int i;
                string welcomeMsg = "Write 'exit' to exit chat\n";
                byte[] msg = Encoding.ASCII.GetBytes(welcomeMsg);
                streamIn.Write(msg, 0, msg.Length);
                while ((i = streamIn.Read(bytes, 0, bytes.Length)) != 0)
                {
                    data = Encoding.ASCII.GetString(bytes, 0, i);
                    string sendingData = nameOfClient + ": " + data;
                    msg = Encoding.ASCII.GetBytes(sendingData);
                    streamOut.Write(msg, 0, msg.Length);
                    Console.WriteLine(data);
                    if (data.Equals("exit\n"))
                    {
                        streamIn.Close();
                        streamOut.Close();
                        Exit();
                        return;
                    }
                }
                streamIn.Close();
                streamOut.Close();
            }
        }

        public void Exit()
        {
            serverIn.Stop();
        }
    }
}
