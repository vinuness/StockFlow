using Estoque.Domain.Entities.Clientes;
using System.Net;
using System.Net.Mail;

namespace Estoque.Domain.Entities
{
    public class EmailSender
    {
        public async Task SendEmail(Cliente cliente)
        {
            var msg = new MailMessage();
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.Timeout = 60_000;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(
                    "stockflow569@gmail.com",
                    ""
                );

                msg.From = new MailAddress("stockflow569@gmail.com", "StockFlow");
                msg.Body = @$"
                        <h3> Olá, {cliente.Nome.ToUpper()}! </h3>

                        Seja muito bem-vindo(a) ao StockFlow.
                        <br>
                        Recebemos sua solicitação de cadastro e estamos felizes em tê-lo(a) conosco.
                        <br>
                        Agradecemos por escolher o StockFlow. Estamos à disposição para oferecer a melhor experiência possível.
                        <br>
                        <br>
                        Atenciosamente,
                        
                        <strong> Equipe StockFlow</strong>
                        <br>
                        <hr>
                        Importante: Este é um e-mail enviado automaticamente. Por favor, não responda esta mensagem.";

                msg.Subject = "Confirmação de cadastro";    
                msg.IsBodyHtml = true;
                msg.Priority = MailPriority.Normal;

                msg.To.Add(cliente.Email);
                await smtpClient.SendMailAsync(msg);
            }
            catch
            {
                throw;
            }
        }
    }
}