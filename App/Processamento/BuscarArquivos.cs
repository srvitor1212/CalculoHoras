using System.Text.RegularExpressions;

namespace CalculoHoras.App.Processamento;

public class BuscarArquivos
{
    public List<string> Arquivos { get; set; } = new List<string>();

    private readonly string LabelPadrao = string.Empty;

    private readonly ConfiguracaoAmbiente _configuracaoAmbiente;

    public BuscarArquivos(ConfiguracaoAmbiente configuracaoAmbiente, string label)
    {
        _configuracaoAmbiente = configuracaoAmbiente;

        LabelPadrao = label;

        GetArquivosParaImportar();
    }



    private void GetArquivosParaImportar()
    {
        var arquivosNaPasta = Directory.GetFiles(_configuracaoAmbiente.PathImportar);

        Arquivos = GetLabelDosArquivosParaImportar(arquivosNaPasta);
    }

    private List<string> GetLabelDosArquivosParaImportar(string[] arquivos)
    {
        var arquivosImportar = new List<string>();

        foreach (var arq in arquivos)
        {
            var labelArquivo = Path.GetFileName(arq);
            if (Regex.IsMatch(labelArquivo, LabelPadrao, RegexOptions.IgnoreCase))
            {
                arquivosImportar.Add(labelArquivo);
                Console.WriteLine($"Arquivo localizado: {labelArquivo}");
            }
        }

        return arquivosImportar;
    }
}
