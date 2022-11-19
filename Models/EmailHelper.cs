using System.Net.Mail;

namespace Astromedia.Models;

public class EmailHelper
{
    public bool SendEmailPasswordReset(string userEmail, string link)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("alexsandro.astromedia@outlook.com");
            mailMessage.To.Add(new MailAddress(userEmail));
 
            mailMessage.Subject = "Password Reset";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = link;

            SmtpClient client = new SmtpClient("smtp-mail.outlook.com");
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            System.Net.NetworkCredential credential = new System.Net.NetworkCredential("alexsandro.astromedia@outlook.com", "astromedia123_"); 
            client.EnableSsl = true;
            client.Credentials = credential;
            try
            {
                client.Send(mailMessage);
                return true; 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
}
