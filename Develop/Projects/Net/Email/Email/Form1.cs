using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;

namespace Email
{
    public partial class Form1 : Form
    {
        SmtpClient client = new SmtpClient();

        public Form1()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Log("Start");

                if (!string.IsNullOrEmpty(textBoxHost.Text))
                    client.Host = textBoxHost.Text;

                if (!string.IsNullOrEmpty(textBoxPort.Text))
                    client.Port = int.Parse(textBoxPort.Text);

                if (!(string.IsNullOrEmpty(textBoxUser.Text) && string.IsNullOrEmpty(textBoxPassword.Text)))
                    client.Credentials = new NetworkCredential(textBoxUser.Text, textBoxPassword.Text);

                client.UseDefaultCredentials = checkBoxDefaultCred.Checked;

                client.Send(GetMessage());

                Log("End");
            }
            catch (Exception ex)
            {
                Log(ex.ToString());
            }
        }

        private MailMessage GetMessage()
        {
            MailMessage message = new MailMessage();


            message.To.Add(new MailAddress(textBoxToEmail.Text, textBoxToDisplayName.Text));
            message.From = new MailAddress(textBoxFromEmail.Text, textBoxFromName.Text);

            if (!string.IsNullOrEmpty(textBoxReplyEmail.Text))
                message.ReplyTo = new MailAddress(textBoxReplyEmail.Text.Trim(), textBoxReplyName.Text.Trim());

            if (!string.IsNullOrEmpty(textBoxSenderEmail.Text))
                message.Sender = new MailAddress(textBoxSenderEmail.Text, textBoxSenderName.Text);

            message.Subject = textBoxSubject.Text;
            message.Body = textBoxBody.Text;
            foreach (HeadersDataSet.HeadersRow row in headersDataSet1.Headers)
            {
                message.Headers.Add(row.Name, row.Value);
            }
  

            return message;
        }


        private void Log(string message)
        {
            textBoxLog.Text = message + Environment.NewLine + textBoxLog.Text;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }


    }
}