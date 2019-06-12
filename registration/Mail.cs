using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace registration
{
    class Mail
    {
        public static void SendEmail(string recipientMail, string senderName, string senderMail, string host,
            int port, string password, string letterSubject, string letterText, string attachFile = null)
        {
            var senderAdress = new MailAddress(senderMail, senderName);
            var recipientAddress = new MailAddress(recipientMail);
            var message = new MailMessage(senderAdress, recipientAddress);
            message.Subject = letterSubject;
            message.Body = letterText;
            if (!string.IsNullOrEmpty(attachFile))
                message.Attachments.Add(new Attachment(attachFile));

            var client = new SmtpClient()
            {
                Host = host,
                Port = port,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderAdress.Address, password),
            };

            client.Send(message);
        }
    }
}
