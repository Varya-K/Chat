using System;
using System.Drawing;
using System.Windows.Forms;


namespace Lab4_Client
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            gB_send_message.Controls.Add(textbox_message);
            gB_send_message.Controls.Add(button_send);
            button_send.Enabled = false;
            y = 10;
           
        }

        int x = 20;
        int y;
        int max_width_label = 300;
        
        public void set_gb_name(string name) //отображается имя клиента
        {
            gB_send_message.Text = name;
        }

        private void button_send_Click(object sender, EventArgs e)
        {
            procEventArgs args = new procEventArgs();
            args.message_text = textbox_message.Text;
            args.message_time = DateTime.Now.ToShortTimeString();
            Send(args);
            
            textbox_message.Text = "";
            create_message(args.message_text, args.message_time);

        }
        protected virtual void Send(procEventArgs e)
        {
            EventHandler<procEventArgs> handler = request_send;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<procEventArgs> request_send;
        public void form_receive_message(string text, string time, string name)//отображает сообщение других пользователей
        {
            create_message(text, time, name, false);
        }
        private void create_message(string text, string time, string name = "", bool send_by_current_client = true) 
        {
            
            Label label_name = new Label();
            panel1.Controls.Add(label_name);
            label_name.Size = new System.Drawing.Size(0, 0);
            label_name.AutoSize = true;
            label_name.MaximumSize = new System.Drawing.Size(max_width_label, 0);
            label_name.Font = new Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold);
            label_name.Text = name;
            
            //panel_chat.Controls.Add(label_name);
            label_name.Location = new Point(x, y);
            if (send_by_current_client) 
            {
                label_name.Visible = false;
            }
            else
            {
                y += label_name.Size.Height;
            }


            

            Label label_message = new Label();
            panel1.Controls.Add(label_message);
            label_message.MaximumSize = new System.Drawing.Size(max_width_label,0);
            label_message.Font = new Font("Microsoft Sans Serif", 9F);
            label_message.Text = text;
            label_message.AutoSize = true;
            label_message.Visible = true;
            if (send_by_current_client)
            {
                
                label_message.Location = new System.Drawing.Point(panel1.Width - label_message.Size.Width - x, y);
            }
            else
            {
                label_message.Location = new System.Drawing.Point(x, y);
            }
            
            //panel_chat.Controls.Add(label_message);
            y += label_message.Height;


            Label label_time = new Label();
            panel1.Controls.Add(label_time);
            label_time.Size = new System.Drawing.Size(0, 0);
            label_time.AutoSize = true;
            label_time.MaximumSize = new System.Drawing.Size(max_width_label, 0);
            label_time.Font = new Font("Microsoft Sans Serif", 8F);
            label_time.ForeColor = Color.DarkGray;
            label_time.Text = time;
            
            if (send_by_current_client)
            {
                label_time.Location = new Point(panel1.Width - label_time.Width - x, y);
            }
            else
            {
                label_time.Location = new Point(x+Math.Max(label_message.Width,label_name.Width)-label_time.Width, y);
            }
            y += label_time.Height+10;

        }

        private void textbox_message_TextChanged(object sender, EventArgs e) 
        {
            if (textbox_message.Text.Length == 0) button_send.Enabled = false;
            else button_send.Enabled = true;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            request_close();
        }
        public delegate void Close();

        public event Close request_close;
    }

    public class procEventArgs : EventArgs //класс данных события отправки сообщения
    {
        public string message_text { get; set; }
        public string message_time { get; set; }
    }
}
