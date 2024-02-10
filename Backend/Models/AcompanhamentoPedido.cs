using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;

namespace Pizzaria.Models;

[PublicAPI]
public class AcompanhamentoPedido
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required(ErrorMessage = "O acompanhamento é obrigatório")]
    public Acompanhamento Acompanhamento { get; set; }
    [Required(ErrorMessage = "A quantidade é obrigatória")]
    [Range(1, 100, ErrorMessage = "A quantidade deve estar entre 1 e 100")]
    public int Quantidade { get; set; }
    public double Preco { get; private set; }

    private AcompanhamentoPedido() { }

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