namespace CalculoHoras.App.DTO;

public record SaldoPorDia(
    DateOnly Data, 
    TimeSpan HoraTrabalhada, 
    TimeSpan Saldo);
