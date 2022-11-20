using System.Net.Mail;

namespace Astromedia.Services;

public class EmailService
{
    public bool SendEmailPasswordReset(string userEmail, string userName, string link)
    {
        MailMessage mailMessage = new MailMessage();
        mailMessage.From = new MailAddress("alexsandro.astromedia@outlook.com");
        mailMessage.To.Add(new MailAddress(userEmail));
        mailMessage.Subject = "Redefinição de senha";
        mailMessage.IsBodyHtml = true;
        // mailMessage.Body = link;
        mailMessage.Body =  "<div style=\"text-align: center;\"><div style=\"padding: 10px; text-align: left\"><h1>Pedido de altera&ccedil;&atilde;o de senha</h1>\n" +
            "<p>Ol&aacute;, "+ userName + ".</p>\n" +
            "<p>Utilize o bot&atilde;o abaixo para alterar a sua senha.</p>\n" +
            "<a href=\"" + link +"\" target=\"_blank\" style=\"max-width: 280px; text-decoration: none; display: inline-block; background-color: #4caf50; color: #ffffff; height: 36px; border-radius: 5px; font-weight: bold; font-size: 18px; margin: 20px 0; width: 100%; text-align: center; padding-top: 10px; \">" +
            "  Alterar Senha" +
            "</a>" +
            "<p>Caso n&atilde;o consiga utilizar o bot&atilde;o, copie e cole o seguinte link no seu navegador:</p>\n" +
            "<p>"+ link + "</p>\n" +
            "<p>Atenciosamente,</p>\n" +
            "<p>Astromedia</p></div></div>";

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

    public bool SendVerificationEmail(string userEmail, string userName, string link)
    {
        MailMessage mailMessage = new MailMessage();
        mailMessage.From = new MailAddress("alexsandro.astromedia@outlook.com");
        mailMessage.To.Add(new MailAddress(userEmail));
        mailMessage.Subject = "Verificação de Email";
        mailMessage.IsBodyHtml = true;
        mailMessage.Body =  "<div style=\"text-align: center;\"><div style=\"padding: 10px; text-align: left\"><h1>Verifique seu e-mail</h1>\n" +
                "<p>Ol&aacute;, "+ userName + ".</p>\n" +
                "<p>Voc&ecirc; realizou o cadastro na Astromedia.</p>\n" +
                "<p>Utilize o bot&atilde;o abaixo para verificar seu email.</p>\n" +
                "<a href=\"" + link +"\" target=\"_blank\" style=\"max-width: 280px; text-decoration: none; display: inline-block; background-color: #4caf50; color: #ffffff; height: 36px; border-radius: 5px; font-weight: bold; font-size: 18px; margin: 20px 0; width: 100%; text-align: center; padding-top: 10px; \">" +
                "  Verificar Email" +
                "</a>" +
                "<p>Caso n&atilde;o consiga utilizar o bot&atilde;o, copie e cole o seguinte link no seu navegador:</p>\n" +
                "<p>"+ link + "</p>\n" +
                "<p>Atenciosamente,</p>\n" +
                "<p>Astromedia</p></div></div>";


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