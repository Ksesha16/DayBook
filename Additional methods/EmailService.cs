using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows;

namespace EvrydayBook.Additional_methods
{
    public class EmailService
    {
        public async Task<string> SendConfirmationEmail(string email, string confirmationCode)
        {
            try
            {
                string smtpServer = "smtp.mail.ru";
                int smtpPort = 587;
                string smtpUsername = "kf12916@mail.ru";
                string smtpPassword = "qqsEBPkhVzZt7JPMAFew";

                using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                {
                    smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    smtpClient.EnableSsl = true;

                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress(smtpUsername);
                        mailMessage.To.Add(email);
                        mailMessage.Subject = "Код подтверждения для EveryDayBook";
                        mailMessage.Body = $@"
                            Здравствуйте! Ваш код подтверждения для доступа к EveryDayBook:<br>
                        <div style='text-align: center;'>   
                            <h2><strong>{confirmationCode}</strong></h2>
                            <img src='https://i.pinimg.com/564x/df/2b/5b/df2b5bd604f9b8b6b81e500c3f4752fe.jpg' width='150' height='150' style='margin-bottom: 20px;' />
                            
                        </div>";
                        mailMessage.IsBodyHtml = true; 

                        try
                        {
                            await smtpClient.SendMailAsync(mailMessage);
                            Console.WriteLine("Сообщение успешно отправлено.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ошибка отправки сообщения: {ex.Message}");
                        }
                    }
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                return $"Ошибка: {ex.Message}";
            }
        }

    }
}
