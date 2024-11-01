namespace CalculoHoras.App.DTO;

public record class LinhaArquivoTexto(
    int codigo,
    string nome,
    string departamento,
    DateTime batida,
    int maquina);