using System.Net.Mail;
using System.Net;
using System.Net.Mime;

namespace DayHospital.Data.Email
{
    public class EmailService
    {
        private EmailService() { }
        const string MyEmailAddress = "MS_N7d28Q@trial-zr6ke4nj1k3gon12.mlsender.net";
        const string MyPassword = "46kf2VBZAubjrRxY";

        readonly static SmtpClient smtpClient = new()
        {
            Host = "smtp.office365.com",
            Port = 587,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(MyEmailAddress, MyPassword),
            EnableSsl = true
        };
        readonly static SmtpClient smtpClient1 = new()
        {
            Host = "smtp.mailersend.net",
            Port = 587,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(MyEmailAddress, MyPassword),
            EnableSsl = true
        };
        private static AlternateView BookingConfirm(string Theater, string Date, string Time, string surgeonName, string email)
        {
            string str = $"<body><div style=\"box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);width: 90%;transform: translate(50%, 50%);font-family: Verdana, sans-serif;font-size:20px;\"><div style=\"padding: 2px 16px;\"><p>Good day.<br/><br/>This is a booking confirmation for a surgery for the {Date} {Time}<br/>Theater: {Theater} <br/>Surgeon Name: {surgeonName} <br/> Surgeon Email: {email}<br/><br>Please Arrive an hour before your appointment. Thank You.</p></div></div></body>";
            AlternateView AV = AlternateView.CreateAlternateViewFromString(str, null, MediaTypeNames.Text.Html);
            return AV;
        }
        public static async Task<bool> Booking(string UserEmail, string Theater, string Date, string Time, string surgeonName, string email)
        {
            MailMessage mailMessage = new()
            {
                From = new MailAddress(MyEmailAddress),
                Subject = "Booking Confirmation",
                IsBodyHtml = true,
                ReplyTo = new MailAddress("s224097084@gmail.com"),
            };
            mailMessage.To.Add(UserEmail);
            mailMessage.AlternateViews.Add(BookingConfirm(Theater, Date, Time, surgeonName, email));
            try
            {
                await smtpClient1.SendMailAsync(mailMessage);

                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
