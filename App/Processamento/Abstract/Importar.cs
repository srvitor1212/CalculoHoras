namespace CalculoHoras.App.Processamento.Abstract;

public abstract class Importar
{
    public string Path { get; private set; } = string.Empty;
    public string Arquivo { get; private set; } = string.Empty;
    public string FullPath { get; private set; }

    protected Importar(string path, string arquivo)
    {
        Path = path;
        Arquivo = arquivo;
        FullPath = $"{Path}\\{Arquivo}";
    }
}
