using System.Globalization;
using CalculoHoras.Application.DTO;

namespace CalculoHoras.App.Processamento;

public class ImportarArquivoAgl
{
    public List<Marcacao> GetMarcacoes(string caminhoArquivo)
    {
        using var leitor = new StreamReader(caminhoArquivo);

        for (var numeroLinha = 1; numeroLinha <= 4; numeroLinha++)
        {
            if (leitor.ReadLine() is null)
                throw new FormatException("Cabeçalho do arquivo AGL incompleto.");
        }

        var cabecalho = leitor.ReadLine()?.Split('\t');
        if (cabecalho is null || cabecalho.Length < 11 ||
            cabecalho[2] != "EnNo" || cabecalho[3] != "Name" || cabecalho[10] != "DateTime")
            throw new FormatException("Cabeçalho do arquivo AGL inválido.");

        var marcacoes = new List<Marcacao>();
        var linhaAtual = 5;
        string? linha;

        while ((linha = leitor.ReadLine()) is not null)
        {
            linhaAtual++;
            if (string.IsNullOrWhiteSpace(linha))
                continue;

            var colunas = linha.Split('\t');
            if (colunas.Length < 11 ||
                !int.TryParse(colunas[2], NumberStyles.Integer, CultureInfo.InvariantCulture, out var id) ||
                !DateTime.TryParseExact(colunas[10].Trim(),
                    new[] { "yyyy-MM-dd  HH:mm:ss", "yyyy-MM-dd HH:mm:ss" },
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var batida))
                throw new FormatException($"Linha {linhaAtual} inválida no arquivo AGL.");

            var nome = string.IsNullOrWhiteSpace(colunas[3])
                ? $"Funcionário {id}"
                : colunas[3].Trim();

            marcacoes.Add(new Marcacao(id, nome, batida));
        }

        return marcacoes;
    }
}
