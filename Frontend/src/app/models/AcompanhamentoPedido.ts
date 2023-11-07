import { Acompanhamento } from "./Acompanhamento";
import {Pedido} from "./Pedido";

export class AcompanhamentoPedido implements Pedido {
  id: number = 0;
  acompanhamento: Acompanhamento = new Acompanhamento();
  quantidade: number = 0;
  preco: number = 0;

  constructor(acompanhamento: Acompanhamento, quantidade: number) {
    this.acompanhamento = acompanhamento;
    this.quantidade = quantidade;
    this.calcularPreco();
  }

  calcularPreco() {
    this.preco = this.acompanhamento.preco * this.quantidade;
  }

  getDescricao(): string {
    return this.acompanhamento.nome;
  }

  getPreco(): number {
    return this.preco;
  }

  getQuantidade(): number {
    return this.quantidade;
  }

}
