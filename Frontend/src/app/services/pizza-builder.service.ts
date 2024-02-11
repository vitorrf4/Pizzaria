import { Injectable } from '@angular/core';
import {Sabor} from "../models/Sabor";
import {Tamanho} from "../models/Tamanho";
import {PizzaPedido} from "../models/PizzaPedido";
import {ServiceResultado} from "../models/ServiceResultado";
import {CalcularPrecoPizza} from "../models/CalcularPrecoPizza";

@Injectable({
  providedIn: 'root'
})
export class PizzaBuilderService {
  private quantidadeSabores = 1;
  private quantidadePizzas = 1;
  private sabores: Sabor[] = [];
  private tamanho: Tamanho = new Tamanho();
  public tamanhoPadrao!: Tamanho;

  constructor() { }

  get pizza() {
    return {sabores: this.sabores, quantidadeSabores: this.quantidadeSabores,
            tamanho: this.tamanho, quantidadePizzas: this.quantidadePizzas,
            preco: this.preco};
  }

  get preco(): number {
    return CalcularPrecoPizza(this.sabores, this.tamanho, this.quantidadePizzas);
  }

  adicionarSabor(sabor: Sabor): ServiceResultado {
    if (this.sabores.length >= this.quantidadeSabores) {
      return {temErros: true, mensagemErro: "Máximo de sabores antigido"}
    }
    if (this.sabores.includes(sabor)) {
      return {temErros: true, mensagemErro: `${sabor.nome} já está presente na pizza`};
    }

    this.sabores.push(sabor);
    return {temErros: false};
  }

  removerSabor(index: number) {
    this.sabores.splice(index,  1);
  }

  mudarQuantidadePizzas(quantidade: number) {
    this.quantidadePizzas = quantidade;
  }

  mudarQuantidadeSabores(quantidade: number) {
    this.quantidadeSabores = quantidade;
  }

  mudarTamanho(tamanho: Tamanho) {
    if (!this.tamanhoPadrao) {
      this.tamanhoPadrao = tamanho;
    }

    if (this.sabores.length > this.tamanho.maxSabores) {
      this.sabores = [];
    }

    this.tamanho = tamanho;
  }

  pizzaEstaValida() : ServiceResultado {
    if (this.pizza.sabores.length <= 0) {
      return {temErros: true, mensagemErro: "Inclua pelo menos um sabor"}
    }

    if (this.quantidadePizzas <= 0) {
      return {temErros: true, mensagemErro: "Quantidade de pizzas inválida"}
    }

    return {temErros: false};
  }

  buildPizza() : PizzaPedido {
    return new PizzaPedido(this.sabores, this.tamanho, this.quantidadePizzas);
  }

  resetPizza() {
    this.tamanho = this.tamanhoPadrao;
    this.sabores = [];
    this.quantidadeSabores = 1;
    this.quantidadePizzas = 1;
  }
}
