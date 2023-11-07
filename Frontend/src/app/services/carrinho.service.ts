import { Injectable } from '@angular/core';
import {Pedido} from "../models/Pedido";
import {BehaviorSubject} from "rxjs";

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
}
