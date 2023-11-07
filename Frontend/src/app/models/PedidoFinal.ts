import { AcompanhamentoPedido } from "./AcompanhamentoPedido";
import { Cliente } from "./Cliente";
import { PizzaPedido } from "./PizzaPedido";
import { Promocao } from "./Promocao";

export class PedidoFinal {
    id: number = 0;
    cliente: Cliente = new Cliente();
    pizzas: PizzaPedido[] = [];
    acompanhamentos: AcompanhamentoPedido[] = [];
    precoTotal: number = 0;
    horaPedido : Date;
    promocao: Promocao | undefined;

    constructor(cliente: Cliente, pizzas: PizzaPedido[], acompanhamentos?: AcompanhamentoPedido[]) {
      this.cliente = cliente;
      this.pizzas = pizzas;
      this.acompanhamentos = acompanhamentos || [];
      this.calcularPreco();

      const date = new Date().toLocaleString("pt-BR", { timeZone: "America/Sao_Paulo"});
      this.horaPedido = new Date(date);
    }

    calcularPreco() {
      this.pizzas.forEach(pizza => this.precoTotal += pizza.preco);

      if (this.acompanhamentos) {
        this.acompanhamentos.forEach(acomp => this.precoTotal += acomp.preco);
      }
    }
}
