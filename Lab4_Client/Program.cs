using System;
using System.Windows.Forms;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Lab4_Client
{
    class Client
    {
        static Form1 form;
        private Socket socket;
        public Client(int port, string address, Form1 _form)
        {
            form = _form;
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
            //создаем сокет
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // подключаемся к удаленному хосту
            socket.Connect(ipPoint);

            // получаем имя клиента от сервера
            StringBuilder builder = new StringBuilder();
            int bytes = 0; // количество полученных байтов за 1 раз
            byte[] data = new byte[255]; // буфер для получаемых данных
            do
            {
                bytes = socket.Receive(data);  // получаем сообщение
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (socket.Available > 0);

            string name = builder.ToString();

            form.Invoke(new Action(() => form.set_gb_name(name)));// устанавливаем имя клиента в форме
            //Используется Invoke, так как форма запущена в другом потоке и при вызове метода напрямую выбрасывается исключение


            Thread thrd = new Thread(receive_message);
            thrd.Name = name;
            thrd.Start(socket);//запускает поток получения сообщения
        }

        public void send_message(object sender, procEventArgs e) //метод вызывается при срабатывания события request_send, он отправляет сообщение на сервер
        {
            
            socket.Send(Encoding.Unicode.GetBytes(e.message_time+"}0{"+e.message_text)); // "}0{" - строка, разделяющая подстроку со временем и текст сообщения
     
        }

        public void close_socket() //метод вызывается при срабатывании события request_close, он закрывает сокет.
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
        private void receive_message(object socket_connect) //метод получения сообщения
        {
            Socket socket = (Socket)socket_connect;
            while (true)
            {
                // готовимся получить имя получателя
                StringBuilder builder = new StringBuilder();
                int bytes = 0; // количество полученных байтов за 1 раз
                int kol_bytes = 0;//количество полученных байтов
                byte[] data = new byte[255]; // буфер для получаемых данных
                do
                {
                    bytes = socket.Receive(data);  // получаем сообщение
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    kol_bytes += bytes;
                }
                while (socket.Available > 0);
                if (kol_bytes == 0) break;

                int first_index = builder.ToString().IndexOf("}0{");
                int second_index = builder.ToString().IndexOf("}1{");
                string name = builder.ToString().Substring(0, first_index);
                string time = builder.ToString().Substring(first_index + 3,second_index-(first_index+3));
                string text = builder.ToString().Substring(second_index + 3);

                form.Invoke(new Action(() => form.form_receive_message(text, time, name))); //вызываем метод формы, отображающий полученной сообщение
                //Используется Invoke, так как форма запущена в другом потоке и при вызове метода напрямую выбрасывается исключение
            }
            close_socket();
        }
    }
    static class Program
    {

        static Form1 form;
        static void run_form()
        {
            Application.Run(form);
        }

        static void Main(string[] args)
        {
            // адрес и порт сервера, к которому будем подключаться
            int port = 8005; // порт сервера
            string address = "127.0.0.1"; // адрес сервера


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form = new Form1();
            Thread thread_client = new Thread(run_form); 
            thread_client.Start(); // запускаем форму в другом потоке

            Client client = new Client(port, address, form); // Создаем клиента, который готов к получению и отправки сообщений
            form.request_send += client.send_message; // при нажатии кнопки "send" в форме срабатывает событие request_send, которое вызывает метод клиента send_message
            form.request_close += client.close_socket; // при закрытии формы срабатывает событие request_close, которое вызывает метод клиента close_socket

        }
    }
}
