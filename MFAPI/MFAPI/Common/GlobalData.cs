using System;
using System.Net.Mail;
using System.Text;

namespace MFAPI.Common
{
    public class GlobalData
    {
        public void SendEmail(string messagebody, string subject, string toAddress)
        {
            MailMessage message = new MailMessage("eternaluppal@gmail.com", toAddress);
            message.Subject = subject;
            message.Body = messagebody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = new System.Net.NetworkCredential("eternaluppal@gmail.com", "98d39ete");            
            try
            {
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }
        public string GetTransactionNo()
        {
            string s = "TR" + String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000);
            return s;
        }
        public string GetAppointmentNo()
        {
            string s = "M" + String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000);
            return s;
        }
    }
}
