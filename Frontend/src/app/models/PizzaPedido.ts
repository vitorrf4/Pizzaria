import { Sabor } from "./Sabor";
import { Tamanho } from "./Tamanho";
import {Pedido} from "./Pedido";

export class PizzaPedido implements Pedido {
  id: number = 0;
  sabores: Sabor[] = [];
  tamanho: Tamanho = new Tamanho();
  preco: number = 0;

  getNome(): string {
    let nomesSabores = "";

    for (let i = 0; i < this.sabores.length; i++) {
      if (i == this.sabores.length - 1) {
        nomesSabores = nomesSabores + this.sabores[i].nome;
        break;
      }
      nomesSabores = nomesSabores + this.sabores[i].nome + ", ";
    }

    return nomesSabores;
  }

  getPreco(): number {
    return this.preco;
  }

  getQuantidade(): number {
    return 0;
  }
}
