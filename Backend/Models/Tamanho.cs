using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace Pizzaria.Models;

[PublicAPI]
public class Tamanho
{
    [Key]
    [Required(ErrorMessage = "O nome é obrigatório")]
    [MaxLength(30, ErrorMessage = "O nome não deve ultrapassar 30 caracteres")]
    public string Nome { get; set; } = "";
    [Required(ErrorMessage = "O multiplicador de preço é obrigatório")]
    [Range(1, 100, ErrorMessage = "O multiplicador deve ser no mínimo 1")]
    public double MultiplicadorPreco { get; set; } = 1;
    [Required(ErrorMessage = "A quantidade de fatias é obrigatória")]
    [Range(1, 50, ErrorMessage = "A quantidade de fatias deve estar entre 1 e 50")]
    public int QntdFatias { get; set; }
    [Required(ErrorMessage = "O máximo de sabores é obrigatório")]
    [Range(1, 10, ErrorMessage = "O máximo de sabores deve estar entre 1 e 10")]
    public int MaxSabores { get; set; }

    private Tamanho() { }

    [JsonConstructor]
    public Tamanho(string nome)
    {
        Nome = nome;
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