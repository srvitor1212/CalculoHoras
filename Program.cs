using CalculoHoras.App.Processamento;
using CalculoHoras.Application.DTO;
using CalculoHoras.Application.Processamento;


bool importarArquivoAgl = true;


Console.WriteLine($"versão 1.2\n\n");
Console.WriteLine($"importar aquivos AGL_00? {importarArquivoAgl}\n\n");

var ambiente = new ConfiguracaoAmbiente();
List<Marcacao> marcacoes;

if (importarArquivoAgl)
{
    var arquivos = new BuscarArquivos(ambiente, @"^AGL_001\.txt$");
    var importar = new ImportarArquivoAgl();

    marcacoes = new List<Marcacao>();

    foreach (var arquivo in arquivos.Arquivos)
    {
        Console.WriteLine($"Processando arquivo {arquivo}");
        marcacoes.AddRange(importar.GetMarcacoes(Path.Combine(ambiente.PathImportar, arquivo)));
    }
}
else
{
    var arquivos = new BuscarArquivos(ambiente, @"Registro de atendimento\(Funcionário-\d+\)\.txt");
    var importar = new ImportarArquivoTexto(
        ambiente.PathImportar,
        "ID\tNome\tDepart.\tTempo\tNúmero da máquina\t");

    marcacoes = new List<Marcacao>();

    foreach (var arquivo in arquivos.Arquivos)
    {
        Console.WriteLine($"Processando arquivo {arquivo}");
        marcacoes.AddRange(importar.GetMarcacoes(Path.Combine(ambiente.PathImportar, arquivo)));
    }
}

var calculo = new CalculoDeHoras(
    marcacoes,
    cargaHoraria: new TimeSpan(0, 8, 30, 0),
    cargaHorariaSabado: new TimeSpan(0, 4, 0, 0));

var resultado = calculo.Calcular();

var visualizar = new CalculoDeSaldo(
    resultado, 
    5);

visualizar.ImprimirSaldoConsole();

Console.ReadKey();
