import { AcompanhamentoPedido } from "./AcompanhamentoPedido";
import { Cliente } from "./Cliente";
import { PizzaPedido } from "./PizzaPedido";
import { Promocao } from "./Promocao";
import {Pedido} from "./Pedido";

export class PedidoFinal {
    id: number = 0;
    cliente: Cliente = new Cliente();
    pizzas: PizzaPedido[] = [];
    acompanhamentos: AcompanhamentoPedido[] = [];
    precoTotal: number = 0;
    horaPedido = new Date();
    promocao: Promocao = new Promocao();

    calcularPreco() {
      this.pizzas.forEach(pizza => this.precoTotal += pizza.preco);

      if (this.acompanhamentos) {
        this.acompanhamentos.forEach(acomp => this.precoTotal += acomp.precoTotal);
      }
    }
}
