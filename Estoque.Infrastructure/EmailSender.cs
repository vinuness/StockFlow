using Estoque.Domain.Entities.Clientes;
using Estoque.Domain.Entities.Pedidos;
using System.Net;
using System.Net.Mail;

namespace Estoque.Infrastructure.Entities
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
                    "uhkvjbfwuraozjzm"
                );

                msg.From = new MailAddress("stockflow569@gmail.com", "StockFlow");
                msg.Body = $@"
                            <!DOCTYPE html>
                            <html lang='pt-BR'>
                            <head>
                                <meta charset='UTF-8'>
                            </head>
                            <body style='margin:0;padding:0;background-color:#f4f4f4;font-family:Arial,Helvetica,sans-serif;'>

                            <table width='100%' cellpadding='0' cellspacing='0' style='background-color:#f4f4f4;padding:20px;'>
                                <tr>
                                    <td align='center'>

                                        <table width='600' cellpadding='0' cellspacing='0' style='background-color:#ffffff;border-radius:8px;overflow:hidden;'>

                                            <tr>
                                                <td align='center' style='background-color:steelblue;padding:20px;color:white;'>
                                                    <h1 style='margin:0;'>STOCKFLOW</h1>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style='padding:40px;color:#333333;font-size:16px;line-height:1.8;'>

                                                    <h2 style='margin-top:0;color:steelblue;'>
                                                        Olá, {cliente.Nome.ToUpper()}!
                                                    </h2>

                                                    <p>
                                                        Seja muito bem-vindo(a) ao <strong>StockFlow</strong>.
                                                    </p>

                                                    <p>
                                                        Recebemos sua solicitação de cadastro e estamos felizes em tê-lo(a) conosco.
                                                    </p>

                                                    <p>
                                                        Agradecemos por escolher o <strong>StockFlow</strong>. Estamos à disposição para oferecer a melhor experiência possível.
                                                    </p>

                                                    <br>

                                                    <p>
                                                        Atenciosamente,
                                                    </p>

                                                    <strong>Equipe StockFlow</strong>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='center' style='background-color:steelblue;padding:15px;color:white;font-size:12px;'>
                                                    Importante: Este é um e-mail enviado automaticamente. Por favor, não responda esta mensagem.
                                                </td>
                                            </tr>

                                        </table>

                                    </td>
                                </tr>
                            </table>

                            </body>
                            </html>";

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

        public async Task EmailPedido(Cliente cliente, Pedido pedido)
        {
            var msg = new MailMessage();

            var total = 0m;
            var linhas = "";

            foreach (var item in pedido.Itens)
            {
                var subtotal = item.Quantidade * item.PrecoUnitario;
                total += subtotal;

                linhas += $@"
                    <tr>
                        <td style='padding:10px;border-bottom:1px solid #ddd;'>{item.Produto.Nome}</td>
                        <td style='padding:10px;border-bottom:1px solid #ddd;'>{item.Produto.Descricao}</td>
                        <td style='padding:10px;border-bottom:1px solid #ddd;'>{item.Produto.Categoria.Nome}</td>
                        <td style='padding:10px;border-bottom:1px solid #ddd;'>{item.Produto.Fornecedor.Nome}</td>
                        <td align='center' style='padding:10px;border-bottom:1px solid #ddd;'>R$ {item.PrecoUnitario:N2}</td>
                        <td align='center' style='padding:10px;border-bottom:1px solid #ddd;'>{item.Quantidade}</td>
                        <td align='center' style='padding:10px;border-bottom:1px solid #ddd;'>R$ {subtotal:N2}</td>
                    </tr>";
            }

            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.Timeout = 60_000;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(
                    "stockflow569@gmail.com",
                    "uhkvjbfwuraozjzm"
                );

                msg.From = new MailAddress("stockflow569@gmail.com", "StockFlow");
                msg.Body = $@"<!DOCTYPE html>
                                    <html lang=""pt-BR"">
                                    <head>
                                        <meta charset=""UTF-8"">
                                    </head>

                                    <body style=""margin:0;padding:20px;background-color:#f4f4f4;font-family:Arial,Helvetica,sans-serif;"">

                                    <table width=""100%"" cellpadding=""0"" cellspacing=""0"">
                                        <tr>
                                            <td align=""center"">

                                                <table width=""900"" cellpadding=""0"" cellspacing=""0""
                                                       style=""background:#ffffff;border:1px solid #ddd;border-radius:8px;overflow:hidden;"">

                                                    <tr>
                                                        <td align=""center""
                                                            style=""background:steelblue;padding:20px;color:white;"">
                                                            <h1 style=""margin:0;"">STOCKFLOW</h1>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style=""padding:30px;"">

                                                            <h2 style=""margin-top:0;color:steelblue;"">
                                                                Olá, {cliente.Nome.ToUpper()}!
                                                            </h2>

                                                            <p>
                                                                Seu pedido foi registrado com sucesso.
                                                            </p>

                                                            <p>
                                                                Confira abaixo os detalhes do seu pedido.
                                                            </p>

                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td style=""padding:0 30px 30px;"">

                                                            <table width=""100%""
                                                                   cellpadding=""10""
                                                                   cellspacing=""0""
                                                                   style=""border:1px solid #ddd;border-collapse:collapse;"">

                                                                <tr>
                                                                    <td width=""180""><strong>Pedido</strong></td>
                                                                    <td>#{pedido.Id}</td>
                                                                </tr>

                                                                <tr style=""background:#f7f7f7;"">
                                                                    <td><strong>Data</strong></td>
                                                                    <td>{pedido.DataPedido:dd/MM/yyyy HH:mm}</td>
                                                                </tr>

                                                                <tr>
                                                                    <td><strong>Status</strong></td>
                                                                    <td>{pedido.Status}</td>
                                                                </tr>

                                                            </table>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style=""padding:0 30px 30px;"">

                                                            <table width=""100%""
                                                                   cellpadding=""0""
                                                                   cellspacing=""0""
                                                                   style=""border-collapse:collapse;"">

                                                                <thead>

                                                                    <tr style=""background:steelblue;color:white;"">

                                                                        <th align=""left"" style=""padding:12px;"">Produto</th>

                                                                        <th align=""left"" style=""padding:12px;"">Descrição</th>

                                                                        <th align=""left"" style=""padding:12px;"">Categoria</th>

                                                                        <th align=""left"" style=""padding:12px;"">Fornecedor</th>

                                                                        <th align=""center"" style=""padding:12px;"">
                                                                            Preço Unitário
                                                                        </th>

                                                                        <th align=""center"" style=""padding:12px;"">
                                                                            Quantidade
                                                                        </th>

                                                                        <th align=""center"" style=""padding:12px;"">
                                                                            Subtotal
                                                                        </th>

                                                                    </tr>

                                                                </thead>

                                                                <tbody>

                                                                    {linhas}

                                                                </tbody>

                                                            </table>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style=""padding:0 30px 30px;"">

                                                            <table width=""100%"">
                                                                <tr>
                                                                    <td align=""right""
                                                                        style=""font-size:22px;
                                                                               font-weight:bold;
                                                                               color:steelblue;"">

                                                                        Total: R$ {total:N2}

                                                                    </td>
                                                                </tr>
                                                            </table>

                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td style=""padding:0 30px 30px;"">

                                                            <p>
                                                                Nosso time iniciará o processamento do seu pedido em breve.
                                                                Você receberá novas atualizações sempre que houver alteração
                                                                no status.
                                                            </p>

                                                            <br>

                                                            <p>
                                                                Atenciosamente,
                                                            </p>

                                                            <strong>Equipe StockFlow</strong>

                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td align=""center""
                                                            style=""background:steelblue;
                                                                   color:white;
                                                                   padding:15px;
                                                                   font-size:12px;"">

                                                            Importante: Este é um e-mail enviado automaticamente.
                                                            Por favor, não responda esta mensagem.

                                                        </td>
                                                    </tr>

                                                </table>

                                            </td>
                                        </tr>
                                    </table>

                                    </body>
                                    </html>";

                msg.Subject = "Confirmação de Pedido";
                msg.IsBodyHtml = true;
                msg.Priority = MailPriority.Normal;

                byte[] pdf = PDFService.GerarPedidoPdf(cliente, pedido);

                msg.Attachments.Add(new Attachment(
                    new MemoryStream(pdf),
                    $"Pedido-{pedido.Id}.pdf",
                    "application/pdf"
                ));
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