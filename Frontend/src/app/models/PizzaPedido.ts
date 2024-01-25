import { Sabor } from "./Sabor";
import { Tamanho } from "./Tamanho";
import {Pedido} from "./Pedido";

export class PizzaPedido implements Pedido {
  id: number = 0;
  sabores: Sabor[] = [];
  tamanho: Tamanho = new Tamanho();
  preco: number = 0;
  quantidade: number;

  constructor(sabores: Sabor[], tamanho: Tamanho, quantidade: number) {
    this.sabores = sabores;
    this.tamanho = tamanho;
    this.quantidade = quantidade;
    this.calcularPreco();
  }

  calcularPreco() {
    this.preco = 0;
    
    this.sabores.forEach(sabor => this.preco += sabor.preco);
    this.preco *= this.tamanho.multiplicadorPreco;

    if (this.sabores.length > 1) 
      this.preco = this.preco / this.sabores.length;

    this.preco *= this.quantidade;
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
}
