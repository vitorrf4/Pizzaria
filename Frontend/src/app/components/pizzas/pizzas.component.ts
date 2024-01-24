import {Component, OnInit} from '@angular/core';
import {Sabor} from "../../models/Sabor";
import {SaborService} from "../../services/sabor.service";
import {Tamanho} from "../../models/Tamanho";
import {TamanhoService} from "../../services/tamanho.service";
import {PizzaPedido} from "../../models/PizzaPedido";
import {CarrinhoService} from "../../services/carrinho.service";

@Component({
  selector: 'app-sabor',
  templateUrl: './pizzas.component.html',
  styleUrls: ['./pizzas.component.css']
})
export class PizzasComponent implements OnInit {
  saboresDB: Sabor[] = [];
  tamanhosDB : Tamanho[] = [];
  tamanhoSelecionado = new Tamanho();
  saboresSelecionados : Sabor[] = [];
  maxQntdSabores : number[] = [];
  quantidadeSabores = 1;
  quantidadePizzas = 1;
  pizza!: PizzaPedido;

  constructor(private saborService: SaborService,
              private tamanhoService: TamanhoService,
              private carrinhoService: CarrinhoService) { }

  ngOnInit() {
    this.saborService.listar().subscribe(resposta => {
      this.saboresDB = resposta;
      this.saboresDB.sort((saborA, saborB) => saborA.preco - saborB.preco);
    });

    this.tamanhoService.listar().subscribe(resposta => {
      this.tamanhosDB = resposta;
      this.tamanhosDB.sort((tamanhoA, tamanhoB) => tamanhoA.qntdFatias - tamanhoB.qntdFatias);

      this.tamanhoSelecionado = this.tamanhosDB[0];
      this.getMaxSabores();
      this.construirPizza();
    });

    this.pizza = new PizzaPedido([], this.tamanhoSelecionado, 1);
  }

  construirPizza() {
    if (this.saboresSelecionados.length > this.tamanhoSelecionado.maxSabores) {
      this.saboresSelecionados = [];
    }

    this.pizza = new PizzaPedido(this.saboresSelecionados, this.tamanhoSelecionado, this.quantidadePizzas);
  }

  getMaxSabores() {
    this.maxQntdSabores = [];

    for (let i = 1; i <= this.tamanhoSelecionado.maxSabores; i++) {
      this.maxQntdSabores.push(i);
    }
  }

  adicionarSabor(sabor: Sabor) {
    if (this.saboresSelecionados.length >= this.quantidadeSabores) {
      alert("Maximo de sabores antigido");
      return;
    }

    if (this.saboresSelecionados.includes(sabor)) {
      alert(`${sabor.nome} já está presente na pizza`);
      return;
    }

    this.saboresSelecionados.push(sabor);
  }

  removerSabor(index: number) {
    this.pizza.sabores.splice(index, 1);
  }

  adicionarAoCarrinho() {
    if (this.pizza.sabores.length <= 0 || this.pizza.quantidade <= 0) {
      alert("Pedido inválido");
      return;
    }

    this.pizza.calcularPreco();
    this.carrinhoService.adicionarNoCarrinho(this.pizza);

    alert("Pizza adicionada!");

    this.limparPedido();
  }

  limparPedido() {
    this.tamanhoSelecionado = this.tamanhosDB[0];
    this.saboresSelecionados = [];
    this.quantidadeSabores = 1;
    this.pizza = new PizzaPedido([], this.tamanhoSelecionado, 1);
  }
}
