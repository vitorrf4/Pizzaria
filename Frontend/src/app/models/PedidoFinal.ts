import { AcompanhamentoPedido } from "./AcompanhamentoPedido";
import { Endereco } from "./Endereco";
import { PizzaPedido } from "./PizzaPedido";

export class PedidoFinal {
    id: number = 0;
    clienteCpf: string;
    endereco: Endereco;
    pizzas: PizzaPedido[] = [];
    acompanhamentos: AcompanhamentoPedido[] = [];
    precoTotal: number = 0;
    horaPedido : Date;

    constructor(clienteCpf: string, endereco: Endereco, 
                pizzas: PizzaPedido[], acompanhamentos: AcompanhamentoPedido[]) {
      this.clienteCpf = clienteCpf;
      this.endereco = endereco;
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
