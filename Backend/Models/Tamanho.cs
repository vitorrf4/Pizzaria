using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pizzaria;

public class Tamanho
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Nome { get; set; }
    public int QntdFatias { get; set; }
    public double MultiplicadorPreco { get; set; }
    public int MaxSabores { get; set; }

    public Tamanho() 
    { 
        Nome = "";
    }

    public Tamanho(string nome, int qntdFatias, int maxSabores, double preco)
    { 
        Nome = nome;
        QntdFatias = qntdFatias;
        MaxSabores = maxSabores;
        MultiplicadorPreco = preco;
    }

    public override string ToString()
    {
        return $"Nome: {Nome} | Quantidade de Fatias: {QntdFatias} | Preço: {MultiplicadorPreco}";
    }
}