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
  //TODO change to builder service
  saboresDB: Sabor[] = [];
  tamanhosDB : Tamanho[] = [];
  tamanhoSelecionado = new Tamanho();
  saboresSelecionados : Sabor[] = [];
  quantidadeSabores = 1;
  quantidadePizzas = 1;
  pizza!: PizzaPedido;

  constructor(private saborService: SaborService,
              private tamanhoService: TamanhoService,
              private carrinhoService: CarrinhoService) { }

  get debug() {
    return this.pizza;
  }

  // 1 - get saboresDb and sort
  // 2 - get tamanhosDb and sort
  // 3 - set tamanhoSelecionado as broto
  // 4 - create pizza

  // Usuario pode:
  // Adicionar sabor
  // Mudar tamanho da pizza
  // Mudar quantidade da pizza
  // Mudar quantidade de sabores
  // Remover sabor

  // Nao permitir:
  // Adicionar um sabor duas vezes
  // Adicionar mais sabores do que o tamanho permite
  // Adicionar pizza com quantidade ou sabor < 1

  // PS:
  // Resetar sabores selecionados ao mudar para um tamanho menor que o selecionado

  ngOnInit() {
    this.saborService.listar().subscribe(resposta => {
      this.saboresDB = resposta;
      this.saboresDB.sort((saborA, saborB) => saborA.preco - saborB.preco);
    });

    this.tamanhoService.listar().subscribe(resposta => {
      this.tamanhosDB = resposta;
      this.tamanhosDB.sort((tamanhoA, tamanhoB) => tamanhoA.qntdFatias - tamanhoB.qntdFatias);

      this.tamanhoSelecionado = this.tamanhosDB[0];
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

  get maxQntdSabores() {
    const max = [];
    for (let i = 1; i <= this.tamanhoSelecionado.maxSabores; i++) {
      max.push(i);
    }

    return max;
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
