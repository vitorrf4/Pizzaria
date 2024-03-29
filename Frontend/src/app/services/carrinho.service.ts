import { Injectable } from '@angular/core';
import {Pedido} from "../models/interfaces/Pedido";
import {BehaviorSubject} from "rxjs";
import {PizzaPedido} from "../models/entities/PizzaPedido";
import {AcompanhamentoPedido} from "../models/entities/AcompanhamentoPedido";

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

  private atualizarQuantidadePedido(pedido: Pedido, index: number) {
    const itensAtualizado = this.itensCarrinho;
    itensAtualizado[index].quantidade += pedido.quantidade;

    this.itensCarrinho$.next(itensAtualizado);
  }

  adicionarNoCarrinho(pedido: Pedido) {
    const indexCarrinho = this.itensCarrinho.findIndex(i => {
      return i.getDescricao() == pedido.getDescricao();
    });

    if (indexCarrinho != -1) {
      return this.atualizarQuantidadePedido(pedido, indexCarrinho);
    }

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
