using CalculoHoras.App.Processamento;
using CalculoHoras.Application.DTO;
using CalculoHoras.Application.Processamento;
Console.WriteLine($"versão 1.0\n\n");


var ambiente = new ConfiguracaoAmbiente();

var arquivos = new BuscarArquivos(ambiente);

var importar = new ImportarArquivoTexto(
    ambiente.PathImportar,
    "ID\tNome\tDepart.\tTempo\tNúmero da máquina\t");

var marcacoes = new List<Marcacao>();

foreach (var arquivo in arquivos.Arquivos)
{
    marcacoes.AddRange(importar.GetMarcacoes(arquivo));
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
