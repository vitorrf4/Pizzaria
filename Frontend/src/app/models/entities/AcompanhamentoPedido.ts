import { Acompanhamento } from "./Acompanhamento";
import {Pedido} from "../interfaces/Pedido";

export class AcompanhamentoPedido implements Pedido {
  id: number = 0;
  acompanhamento: Acompanhamento = new Acompanhamento();
  quantidade: number = 0;

  constructor(acompanhamento: Acompanhamento, quantidade: number) {
    this.acompanhamento = acompanhamento;
    this.quantidade = quantidade;
  }

  get preco() {
    return this.acompanhamento.preco * this.quantidade;
  }

  getDescricao(): string {
    return this.acompanhamento.nome;
  }

  toJSON() {
    return {
      id: this.id,
      acompanhamento: this.acompanhamento,
      quantidade: this.quantidade,
      preco: this.preco
    }
  }
}
