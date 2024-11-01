namespace CalculoHoras.App.Auxiliar;

public static class DiaDaSemana
{
    public static string Get(DateTime data) =>
        data.DayOfWeek switch
        {
            DayOfWeek.Monday => "Seg.",
            DayOfWeek.Tuesday => "Ter.",
            DayOfWeek.Wednesday => "Qua.",
            DayOfWeek.Thursday => "Qui.",
            DayOfWeek.Friday => "Sex.",
            DayOfWeek.Saturday => "Sáb.",
            DayOfWeek.Sunday => "Dom.",
            _ => throw new InvalidDataException("Dia da semana não foi reconhecido")
        };
}
