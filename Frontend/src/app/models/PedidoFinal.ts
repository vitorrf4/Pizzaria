import { AcompanhamentoPedido } from "./AcompanhamentoPedido";
import { Cliente } from "./Cliente";
import { PizzaPedido } from "./PizzaPedido";
import { Promocao } from "./Promocao";

export class PedidoFinal {
    id: number = 0;
    cliente: Cliente = new Cliente();
    pizzas: PizzaPedido = new PizzaPedido();
    acompanhamentos: AcompanhamentoPedido = new AcompanhamentoPedido();
    precoTotal: number = 0;
    horaPedido: string = "";
    promocao: Promocao = new Promocao();
}
