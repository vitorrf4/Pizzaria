import { PedidoFinal } from "./PedidoFinal";
import { Sabor } from "./Sabor";
import { Tamanho } from "./Tamanho";

export class PizzaPedido{
    id: number = 0;
    sabores: Sabor = new Sabor();
    tamanho: Tamanho = new Tamanho();
    preco: number = 0;
    pedidoFinal: PedidoFinal = new PedidoFinal();
}