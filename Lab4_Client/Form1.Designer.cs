
namespace Lab4_Client
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.textbox_message = new System.Windows.Forms.TextBox();
            this.button_send = new System.Windows.Forms.Button();
            this.gB_send_message = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gB_send_message.SuspendLayout();
            this.SuspendLayout();
            // 
            // textbox_message
            // 
            this.textbox_message.Location = new System.Drawing.Point(12, 26);
            this.textbox_message.Multiline = true;
            this.textbox_message.Name = "textbox_message";
            this.textbox_message.Size = new System.Drawing.Size(415, 66);
            this.textbox_message.TabIndex = 1;
            this.textbox_message.TextChanged += new System.EventHandler(this.textbox_message_TextChanged);
            // 
            // button_send
            // 
            this.button_send.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_send.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_send.Location = new System.Drawing.Point(444, 34);
            this.button_send.Name = "button_send";
            this.button_send.Size = new System.Drawing.Size(75, 52);
            this.button_send.TabIndex = 2;
            this.button_send.Text = "Send";
            this.button_send.UseVisualStyleBackColor = true;
            this.button_send.Click += new System.EventHandler(this.button_send_Click);
            // 
            // gB_send_message
            // 
            this.gB_send_message.Controls.Add(this.button_send);
            this.gB_send_message.Controls.Add(this.textbox_message);
            this.gB_send_message.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gB_send_message.Location = new System.Drawing.Point(0, 451);
            this.gB_send_message.Name = "gB_send_message";
            this.gB_send_message.Size = new System.Drawing.Size(531, 98);
            this.gB_send_message.TabIndex = 4;
            this.gB_send_message.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(531, 451);
            this.panel1.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 549);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gB_send_message);
            this.Name = "Form1";
            this.Text = "Chat";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.gB_send_message.ResumeLayout(false);
            this.gB_send_message.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox textbox_message;
        private System.Windows.Forms.Button button_send;
        private System.Windows.Forms.GroupBox gB_send_message;
        private System.Windows.Forms.Panel panel1;
    }
}

