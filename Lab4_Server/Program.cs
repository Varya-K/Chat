using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;

namespace SocketTcpServer
{

    class Server
    {
        private Socket listenSocket;
        private List<Socket> handlers = new List<Socket>();
        public Server(int port)
        {
            //получаем адреса для запуска сокета
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);

            // создаем сокет сервера
            listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // связываем сокет с локальной точкой, по которой будем принимать данные
            listenSocket.Bind(ipPoint);
            // начинаем прослушивание

        }

        public void Run()
        {
            int num = 0;
            List<Thread> threads = new List<Thread>();

            listenSocket.Listen(10);

            Console.WriteLine("Сервер запущен. Ожидание подключений...");

            while (true)
            {
                Socket handler = listenSocket.Accept();  // сокет для связи с клиентом
                num++;
                Thread thrd = new Thread(processing_client);
                thrd.Name = "Client " + num.ToString();

                handler.Send(Encoding.Unicode.GetBytes(thrd.Name));

                handlers.Add(handler);

                thrd.Start(handler);
                Console.WriteLine("К серверу присоединился " + thrd.Name+"\n");
            }
        }

        private void processing_client(object handler_socket)
        {

            Socket handler = (Socket)handler_socket;
            // готовимся  получать  сообщение
            while (true)
            {

                StringBuilder builder = new StringBuilder();
                int bytes = 0; // количество полученных байтов за 1 раз
                int kol_bytes = 0;//количество полученных байтов
                byte[] data = new byte[255]; // буфер для получаемых данных
                do
                {
                    bytes = handler.Receive(data);  // получаем сообщение
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    kol_bytes += bytes;
                }
                while (handler.Available > 0);
                if (kol_bytes == 0) break;

                string time = builder.ToString().Substring(0,builder.ToString().IndexOf("}0{"));
                string text = builder.ToString().Substring(builder.ToString().IndexOf("}0{")+3);
                
                // отправляем всем клиентам, то, что получили от этого клиента
                foreach (Socket socket in handlers)
                {
                    if (socket != handler)
                    {
                        socket.Send(Encoding.Unicode.GetBytes(Thread.CurrentThread.Name+"}0{"+time+"}1{"+text));
                    }
                }
                Console.WriteLine(Thread.CurrentThread.Name);
                Console.WriteLine(time + ": " + text);
                Console.WriteLine(kol_bytes + " bytes\n");

            }
            // закрываем сокет
            handlers.Remove(handler);
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
            Console.WriteLine(Thread.CurrentThread.Name + " был отсоединен\n");

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            String Host = Dns.GetHostName();
            Console.WriteLine("Comp name = " + Host);
            IPAddress[] IPs;
            IPs = Dns.GetHostAddresses(Host);
            foreach (IPAddress ip1 in IPs)
                Console.WriteLine(ip1);
            Console.WriteLine();

            int port = 8005; // порт для приема входящих запросов
            Server server = new Server(port);
            server.Run();

        }
    }
}


