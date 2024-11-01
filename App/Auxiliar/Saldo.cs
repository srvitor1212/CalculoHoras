namespace CalculoHoras.App.Auxiliar;
public static class Saldo
{
    public static TimeSpan Calcular(TimeSpan horaTrabalhada, TimeSpan cargaHoraria)
    {
        var trabalhado = new TimeSpan(0, horaTrabalhada.Hours, horaTrabalhada.Minutes, horaTrabalhada.Seconds);

        var carga = new TimeSpan(0, cargaHoraria.Hours, cargaHoraria.Minutes, cargaHoraria.Seconds);

        var result = trabalhado - carga;

        return result;
    }
}
