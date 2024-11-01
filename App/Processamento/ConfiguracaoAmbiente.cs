namespace CalculoHoras.App.Processamento;

public class ConfiguracaoAmbiente
{

    public string Path { get; private set; } = string.Empty;
    public string PathImportar { get; private set; } = string.Empty;

    public ConfiguracaoAmbiente()
    {
        CriarPastas();
    }

    private void CriarPastas()
    {
        Path = Directory.GetCurrentDirectory();

        PathImportar = Path + @"\importar";

        Directory.CreateDirectory(PathImportar);

        Console.WriteLine($"\n-- Criação de pastas --\n" +
            $"[{Path}]\n" +
            $"[{PathImportar}]\n");
    }
}
