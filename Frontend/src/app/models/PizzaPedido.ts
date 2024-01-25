import { Sabor } from "./Sabor";
import { Tamanho } from "./Tamanho";
import {Pedido} from "./Pedido";

export class PizzaPedido implements Pedido {
  id: number = 0;
  sabores: Sabor[] = [];
  tamanho: Tamanho = new Tamanho();
  quantidade: number = 1;

  constructor(sabores: Sabor[], tamanho: Tamanho, quantidade: number) {
    this.sabores = sabores;
    this.tamanho = tamanho;
    this.quantidade = quantidade;
  }

  get preco() {
    let total = 0;
    
    this.sabores.forEach(sabor => total += sabor.preco);
    total *= this.tamanho.multiplicadorPreco;

    if (this.sabores.length > 1) {
      total /= this.sabores.length;
    }

    return total *= this.quantidade;
  }

  getDescricao(): string {
    const tamanho = `Pizza ${this.tamanho.nome} de `;
    let nomesSabores = "";

    for (let i = 0; i < this.sabores.length; i++) {
      if (i == this.sabores.length - 1) {
        nomesSabores = nomesSabores + this.sabores[i].nome;
        break;
      }
      nomesSabores = nomesSabores + this.sabores[i].nome + ", ";
    }

    return tamanho + nomesSabores;
  }
  
  toJSON() {
    return {
      id: this.id,
      sabores: this.sabores,
      tamanho: this.tamanho,
      quantidade: this.quantidade,
      preco: this.preco
    }
  }
}
