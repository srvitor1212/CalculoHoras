using CalculoHoras.App.Auxiliar;
using CalculoHoras.App.DTO;
using CalculoHoras.App.Enum;
using CalculoHoras.Application.DTO;

namespace CalculoHoras.Application.Processamento;

public class CalculoDeHoras
{
    public List<Marcacao> Marcacoes { get; private set; } = new List<Marcacao>();

    public TimeSpan CargaHoraria { get; private set; }

    public TimeSpan CargaHorariaSabado { get; private set; }



    public CalculoDeHoras(List<Marcacao> marcacoes, TimeSpan cargaHoraria, TimeSpan cargaHorariaSabado)
    {
        Marcacoes = marcacoes;
        CargaHoraria = cargaHoraria;
        CargaHorariaSabado = cargaHorariaSabado;
    }



    public List<Resultado> Calcular()
    {
        var result = new List<Resultado>();

        var todosOsFuncionarios = Marcacoes.GroupBy(x => x.Id);

        foreach (var funcionario in todosOsFuncionarios)
        {
            var diasComBatida = funcionario.GroupBy(x => x.Batida.Date);

            var horasFuncionario = CalcularHorasFuncionario(diasComBatida);

            result.Add(new Resultado(
                funcionario.FirstOrDefault()!.Id,
                funcionario.FirstOrDefault()!.Nome,
                horasFuncionario));
        }

        return result;
    }

    private List<SaldoPorDia> CalcularHorasFuncionario(IEnumerable<IGrouping<DateTime, Marcacao>> diasComBatida)
    {
        var result = new List<SaldoPorDia>();

        foreach (var dia in diasComBatida)
        {
            var horasNoDia = CalcularHorasDia(dia);

            TimeSpan saldo = TimeSpan.Zero;

            if (dia.Key.DayOfWeek == DayOfWeek.Saturday) //sábado
            {
                saldo = Saldo.Calcular(horasNoDia, CargaHorariaSabado);
            }
            else
            {
                saldo = Saldo.Calcular(horasNoDia, CargaHoraria);
            }

            result.Add(new SaldoPorDia(
                new DateOnly(dia.Key.Year, dia.Key.Month, dia.Key.Day), 
                new TimeSpan(horasNoDia.Hours, horasNoDia.Minutes, 0), 
                saldo));
        }

        return result;
    }

    private TimeSpan CalcularHorasDia(IGrouping<DateTime, Marcacao> dia)
    {

        var entradaSaida = TipoRegistroEnum.Entrada;
        var entrada = DateTime.MinValue;
        var saida = DateTime.MinValue;

        var horaTrabalhada = TimeSpan.Zero;

        foreach (var batida in dia)
        {
            if (entradaSaida == TipoRegistroEnum.Entrada)
            {
                entrada = batida.Batida;
                entradaSaida = Alternar(entradaSaida);
                continue;
            }

            if (entradaSaida == TipoRegistroEnum.Saida)
            {
                saida = batida.Batida;
                entradaSaida = Alternar(entradaSaida);
            }

            if (entrada != DateTime.MinValue && saida != DateTime.MinValue)
                horaTrabalhada = horaTrabalhada + (saida - entrada);
        }

        return horaTrabalhada;
    }


    private static TipoRegistroEnum Alternar(TipoRegistroEnum tipoRegistro)
        => tipoRegistro == TipoRegistroEnum.Entrada
            ? TipoRegistroEnum.Saida
            : TipoRegistroEnum.Entrada;
}

