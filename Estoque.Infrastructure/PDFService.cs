using Estoque.Domain.Entities.Clientes;
using Estoque.Domain.Entities.Pedidos;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Estoque.Infrastructure
{
    public class PDFService
    {
        public static byte[] GerarPedidoPdf(Cliente cliente, Pedido pedido)
        {
            return Document.Create(document =>
            {
                document.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    page.Header().Column(column =>
                    {
                        column.Item().Text("STOCKFLOW")
                            .Bold()
                            .FontSize(24)
                            .FontColor(Colors.Blue.Medium);

                        column.Item().Text("Comprovante do Pedido");
                    });

                    page.Content().Column(column =>
                    {
                        column.Spacing(10);

                        column.Item().Text($"Cliente: {cliente.Nome}");
                        column.Item().Text($"E-mail: {cliente.Email}");

                        column.Item().Text($"Pedido: #{pedido.Id}");
                        column.Item().Text($"Data: {pedido.DataPedido:dd/MM/yyyy HH:mm}");
                        column.Item().Text($"Status: {pedido.Status}");

                        column.Item().PaddingVertical(10);

                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(4); // Produto
                                columns.RelativeColumn(1); // Quantidade
                                columns.RelativeColumn(2); // Valor
                                columns.RelativeColumn(2); // Subtotal
                            });

                            table.Header(header =>
                            {
                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Produto").Bold();
                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Qtd").Bold();
                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Valor").Bold();
                                header.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Subtotal").Bold();
                            });

                            decimal total = 0;

                            foreach (var item in pedido.Itens)
                            {
                                var subtotal = item.Quantidade * item.PrecoUnitario;
                                total += subtotal;

                                table.Cell().Padding(5).Text(item.Produto.Nome);
                                table.Cell().Padding(5).Text(item.Quantidade.ToString());
                                table.Cell().Padding(5).Text($"R$ {item.PrecoUnitario:N2}");
                                table.Cell().Padding(5).Text($"R$ {subtotal:N2}");
                            }

                            table.Cell().ColumnSpan(3)
                                .AlignRight()
                                .PaddingTop(10)
                                .Text("Total:")
                                .Bold();

                            table.Cell()
                                .PaddingTop(10)
                                .Text($"R$ {total:N2}")
                                .Bold();
                        });
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text("Obrigado por comprar no StockFlow!");
                });
            }).GeneratePdf();
        }
    }
}