namespace pizzaria;
using System;
using System.Collections;

public class PedidoFinal 
{
    private int id;
    private List<PizzaPedido> pizzas = new();
    private List<AcompanhamentoPedido> acompanhamentos;
    private int precoTotal;
    private int horaPedido;
}