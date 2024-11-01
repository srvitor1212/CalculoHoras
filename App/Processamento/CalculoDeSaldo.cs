using CalculoHoras.App.DTO;

namespace CalculoHoras.App.Processamento;

public class CalculoDeSaldo
{
    public List<Resultado> Calculado { get; set; } = [];
    public int MinutosDeTolerancia { get; set; } = 5;
    public TimeSpan MinutosDeToleranciaExtra { get; private set; } = TimeSpan.FromMinutes(0);
    public TimeSpan MinutosDeToleranciaFalta { get; private set; } = TimeSpan.FromMinutes(0);


    public CalculoDeSaldo(List<Resultado> calculado, int minutosDeTolerancia)
    {
        Calculado = calculado;

        MinutosDeTolerancia = minutosDeTolerancia;

        MinutosDeToleranciaExtra = TimeSpan.FromMinutes(MinutosDeTolerancia);

        MinutosDeToleranciaFalta = TimeSpan.FromMinutes(-MinutosDeTolerancia);
    }


    public void ImprimirSaldoConsole()
    {
        Console.WriteLine("\n\nCalculo de banco de Horas - Resumido");

        foreach (var item in Calculado)
        {
            Console.WriteLine($"\n\n--- {item.Nome} ---");

            TimeSpan saldoTotal = TimeSpan.Zero;

            foreach (var dia in item.SaldoPorDias)
            {
                Console.WriteLine($"{dia.Data} {dia.HoraTrabalhada} {dia.Saldo}");

                if (dia.Saldo < MinutosDeToleranciaFalta)
                    saldoTotal += dia.Saldo;
                else if (dia.Saldo > MinutosDeToleranciaExtra)
                    saldoTotal += dia.Saldo;
            }

            Console.WriteLine($"--------------\nSaldo do período: {saldoTotal}");
        }

    }
}
