import { Injectable } from '@angular/core';
import {Pedido} from "../models/Pedido";
import {BehaviorSubject} from "rxjs";
import {PizzaPedido} from "../models/PizzaPedido";
import {AcompanhamentoPedido} from "../models/AcompanhamentoPedido";
import {Acompanhamento} from "../models/Acompanhamento";

@Injectable({
  providedIn: 'root'
})
export class CarrinhoService {
  quantidadeItensCarrinho = new BehaviorSubject<number>(0);
  itensCarrinho: Pedido[]  = [];

  constructor() {
    this.quantidadeItensCarrinho.next(this.itensCarrinho.length);
  }

  adicionarNoCarrinho(pedido: Pedido) {
    this.itensCarrinho.push(pedido);
    this.quantidadeItensCarrinho.next(this.quantidadeItensCarrinho.value + 1);
  }

  removerDoCarrinho(index: number) {
    this.itensCarrinho.splice(index, 1);
    this.quantidadeItensCarrinho.next(this.quantidadeItensCarrinho.value - 1);
  }

  limparCarrinho() {
    this.itensCarrinho = [];
    this.quantidadeItensCarrinho.next(0);
  }

  filtrarPizzasNoCarrinho() {
    const pizzas : PizzaPedido[] = [];

    this.itensCarrinho.forEach(pedido => {
      if (pedido instanceof PizzaPedido) {
        pizzas.push(pedido);
      }
    });

    return pizzas;
  }

  filtrarAcompanhamentosNoCarrinho() {
    const acompanhamentos : AcompanhamentoPedido[] = [];

    this.itensCarrinho.forEach(pedido => {
      if (pedido instanceof  AcompanhamentoPedido) {
        acompanhamentos.push(pedido);
      }
    });
    return acompanhamentos;
  }
}
