import { AcompanhamentoPedido } from "./AcompanhamentoPedido";
import { Cliente } from "./Cliente";
import { PizzaPedido } from "./PizzaPedido";
import { Promocao } from "./Promocao";

export class PedidoFinal {
    id: number = 0;
    cliente: Cliente;
    pizzas: PizzaPedido[];
    acompanhamentos: AcompanhamentoPedido[];
    precoTotal: number = 0;
    horaPedido : Date;
    promocao: Promocao | undefined;

    constructor(cliente?: Cliente, pizzas?: PizzaPedido[], acompanhamentos?: AcompanhamentoPedido[]) {
      this.cliente = cliente || new Cliente();
      this.pizzas = pizzas || [];
      this.acompanhamentos = acompanhamentos || [];
      this.horaPedido = new Date();
      this.calcularPreco();
    }

    calcularPreco() {
      this.pizzas.forEach(pizza => this.precoTotal += pizza.preco);

      if (this.acompanhamentos) {
        this.acompanhamentos.forEach(acomp => this.precoTotal += acomp.preco);
      }
    }
}
