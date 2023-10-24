using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pizzaria;

public class Promocao
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("Promocao")]
    public int PedidoFinalId { get; set; }
    public int Desconto { get; set; }

    public Promocao() { }

    public Promocao(int pedidoFinalId, int desconto)
    {
        PedidoFinalId = pedidoFinalId;
        Desconto = desconto;
    } 
}