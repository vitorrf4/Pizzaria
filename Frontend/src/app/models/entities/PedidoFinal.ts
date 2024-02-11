import { AcompanhamentoPedido } from "./AcompanhamentoPedido";
import { PizzaPedido } from "./PizzaPedido";
import {Cliente} from "./Cliente";

export class PedidoFinal {
    id: number = 0;
    cliente: Cliente;
    pizzas: PizzaPedido[] = [];
    acompanhamentos: AcompanhamentoPedido[] = [];
    precoTotal: number = 0;
    horaPedido : Date;

    constructor(cliente: Cliente, pizzas: PizzaPedido[],
                acompanhamentos: AcompanhamentoPedido[]) {
      this.cliente = cliente;
      this.pizzas = pizzas;
      this.acompanhamentos = acompanhamentos;
      this.horaPedido = new Date();
      this.calcularPreco();
    }

    calcularPreco() {
      this.pizzas.forEach(pizza => this.precoTotal += pizza.preco);
      this.acompanhamentos.forEach(acomp => this.precoTotal += acomp.preco);
    }
}
