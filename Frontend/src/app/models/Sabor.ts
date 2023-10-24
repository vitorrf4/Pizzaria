import { PizzaPedido } from "./PizzaPedido";

export class Sabor {
    id: number = 0;
    nome: string = "";
    preco: number = 0;
    pedidos: PizzaPedido = new PizzaPedido();
}
