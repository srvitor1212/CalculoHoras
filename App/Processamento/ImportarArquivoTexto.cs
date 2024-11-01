using CalculoHoras.App.DTO;
using CalculoHoras.Application.DTO;

namespace CalculoHoras.App.Processamento;

public class ImportarArquivoTexto
{
    public string Path { get; private set; } = string.Empty;
    public string NomeArquivo { get; private set; } = string.Empty;
    public string FullPath { get; private set; } = string.Empty;
    public string LayoutCabecalho { get; private set; } = string.Empty;

    public ImportarArquivoTexto(string path, string layoutCabecalho)
    {
        Path = path;
        LayoutCabecalho = layoutCabecalho;
    }

    public List<Marcacao> GetMarcacoes(string nomeArquivo)
    {
        SetNomeArquivo(nomeArquivo);

        var linhas = ImportarArquivo();

        var result = linhas.Select(x
            => new Marcacao(x.codigo, x.nome, x.batida)
            ).ToList();

        return result;
    }

    public void SetNomeArquivo(string value)
    {
        NomeArquivo = value;
        FullPath = $"{Path}\\{NomeArquivo}";
    }

    private List<LinhaArquivoTexto> ImportarArquivo()
    {
        if (!FileIsOk())
            return new List<LinhaArquivoTexto>();

        var result = new List<LinhaArquivoTexto>();

        var arquivo = File.ReadAllLines(FullPath);

        foreach (var linha in arquivo)
        {
            if (linha == LayoutCabecalho)
                continue;

            var colunasArquivoOriginal = linha.Split('\t');

            var colunas = ConverterColunas(colunasArquivoOriginal);

            result.Add(colunas);
        }

        return result;
    }

    private bool FileIsOk()
    {
        if (!File.Exists(FullPath))
        {
            Console.WriteLine($"ERRO : Arquivo não encontrado {NomeArquivo}");
            return false;
        }

        var linhas = File.ReadAllLines(FullPath);

        if (linhas.Length == 0)
        {
            Console.WriteLine($"ERRO : Arquivo vazio {NomeArquivo}");
            return false;
        }

        if (linhas.FirstOrDefault() != LayoutCabecalho)
        {
            Console.WriteLine($"ERRO : Cabeçalho do arquivo inválido {NomeArquivo}");
            return false;
        }

        return true;
    }
    private LinhaArquivoTexto ConverterColunas(string[] colunas)
    {
        var code = Convert.ToInt32(colunas[0]);

        var nome = Convert.ToString(colunas[1]);

        var departamento = Convert.ToString(colunas[2]);

        var batida = Convert.ToDateTime(colunas[3]);

        var maquina = Convert.ToInt32(colunas[4]);

        return new LinhaArquivoTexto(code, nome, departamento, batida, maquina);
    }
}
