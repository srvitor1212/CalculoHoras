namespace CalculoHoras.App.DTO;

public record Resultado(
    int Id, 
    string Nome, 
    List<SaldoPorDia> SaldoPorDias);