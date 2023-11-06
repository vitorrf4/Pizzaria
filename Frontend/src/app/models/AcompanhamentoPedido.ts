import { Acompanhamento } from "./Acompanhamento";
import {Pedido} from "./Pedido";

export class AcompanhamentoPedido implements Pedido {
  id: number = 0;
  acompanhamento: Acompanhamento = new Acompanhamento();
  quantidade: number = 0;
  precoTotal: number = 0;

  constructor(acompanhamento: Acompanhamento, quantidade: number) {
    this.acompanhamento = acompanhamento;
    this.quantidade = quantidade;
    this.calcularPreco();
  }

  getDescricao(): string {
    return this.acompanhamento.nome;
  }

  getPreco(): number {
    return this.precoTotal;
  }

  getQuantidade(): number {
    return this.quantidade;
  }

  calcularPreco() {
    this.precoTotal = this.acompanhamento.preco * this.quantidade;
  }
}
