namespace CalculoHoras.Application.DTO;

public class Marcacao
{
    public int Id { get; private set; }
    public string Nome { get; private set; }
    public DateTime Batida { get; private set; }

    public Marcacao(int id, string nome, DateTime dataHora)
    {
        Id = id;
        Nome = nome;
        Batida = dataHora;
    }
}
