using System.Net.Mail;
using System.Net;

namespace Just_project.Models
{
    public class EmailSender : IMessage
    {
        public bool sendMessage(string to, string messageBody, string subject)
        {
            var fromAddress = new MailAddress("khanekshakh@gmail.com", "From Name");
            var toAddress = new MailAddress(to, "To Name");
            const string fromPassword = "***";

            var smtp = new SmtpClient
            {
                Host = "smtp.yandex.ru",
                Port = 465,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = messageBody
            })
            {
                try
                {
                    smtp.Send(message);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

            }
        }
    }
}
