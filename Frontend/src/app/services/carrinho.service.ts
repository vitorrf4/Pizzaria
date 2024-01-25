import { Injectable } from '@angular/core';
import {Pedido} from "../models/Pedido";
import {BehaviorSubject} from "rxjs";
import {PizzaPedido} from "../models/PizzaPedido";
import {AcompanhamentoPedido} from "../models/AcompanhamentoPedido";

@Injectable({
  providedIn: 'root'
})
export class CarrinhoService {
  quantidadeItensCarrinho = new BehaviorSubject<number>(0);
  itensCarrinho$ = new BehaviorSubject<Pedido[]>([]);

  constructor() {
    this.itensCarrinho$.subscribe(() => {
      this.quantidadeItensCarrinho.next(this.itensCarrinho.length);
    });
  }

  get itensCarrinho() {
    return this.itensCarrinho$.value;
  }

  adicionarNoCarrinho(pedido: Pedido) {
    const novoCarrinho = this.itensCarrinho;
    novoCarrinho.push(pedido);

    this.itensCarrinho$.next(novoCarrinho);
  }

  removerDoCarrinho(index: number) {
    const novoCarrinho = this.itensCarrinho;
    novoCarrinho.splice(index, 1);
    
    this.itensCarrinho$.next(novoCarrinho);
  }

  limparCarrinho() {
    this.itensCarrinho$.next([]);
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
