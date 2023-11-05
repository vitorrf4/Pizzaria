import { AcompanhamentoPedido } from "./AcompanhamentoPedido";
import { Cliente } from "./Cliente";
import { PizzaPedido } from "./PizzaPedido";
import { Promocao } from "./Promocao";

export class PedidoFinal {
    id: number = 0;
    cliente: Cliente = new Cliente();
    pizzas: PizzaPedido[] = [];
    acompanhamentos: AcompanhamentoPedido[] | undefined;
    precoTotal: number = 0;
    horaPedido: number;
    promocao: Promocao = new Promocao();

    constructor(cliente: Cliente, pizzas: PizzaPedido[], acompanhamentos?: AcompanhamentoPedido[]) {
      this.cliente = cliente;
      this.pizzas = pizzas;
      this.acompanhamentos = acompanhamentos || undefined;
      this.horaPedido = Date.now();
      this.calcularPreco();
    }

    calcularPreco() {
      this.pizzas.forEach(pizza => this.precoTotal += pizza.preco);

      if (this.acompanhamentos) {
        this.acompanhamentos.forEach(acomp => this.precoTotal += acomp.precoTotal);
      }
    }
}
