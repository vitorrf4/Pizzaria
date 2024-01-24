using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pizzaria;

public class AcompanhamentoPedido
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public Acompanhamento Acompanhamento { get; set; } = new Acompanhamento();
    public int Quantidade { get; set; }
    public double Preco { get; set; }

    public AcompanhamentoPedido() { }

    public AcompanhamentoPedido(Acompanhamento acompanhamento, int quantidade)
    {
        Acompanhamento = acompanhamento;
        Quantidade = quantidade;
        CalcularPreco();
    }

    public void CalcularPreco()
    {
        Preco = Acompanhamento.Preco * Quantidade;
    }

    public override string ToString()
    {
        return $"Acompanhamento: {Acompanhamento.Nome} | " +
                $"Preço Unitário: R${Acompanhamento.Preco} | " +
                $"Quantidade: {Quantidade} | " +
                $"Preço Total do Acompanhamento: R${Preco}";
    }
}