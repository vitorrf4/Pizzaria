import {Component} from '@angular/core';
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
export class PizzasComponent {
  saboresDB: Sabor[] = [];
  tamanhosDB : Tamanho[] = [];
  tamanho = new Tamanho();
  saboresSelecionados : Sabor[] = [];
  maxQntdSabores : number[] = [];
  quantidadeSabores = 1;
  pizza! : PizzaPedido;

  constructor(private saborService: SaborService,
              private tamanhoService: TamanhoService,
              private carrinhoService: CarrinhoService) {
    this.saborService.listar().subscribe(resposta => {
      this.saboresDB = resposta;
    });

    this.tamanhoService.listar().subscribe(resposta => {
      this.tamanhosDB = resposta;
      this.ordernarTamanhosPorQntdFatias();
    });
  }

  construirPizza() {
    this.pizza = new PizzaPedido(this.saboresSelecionados, this.tamanho, 1);
  }

  getMaxSabores() {
    this.maxQntdSabores = [];

    for (let i = 1; i <= this.tamanho.maxSabores; i++) {
      this.maxQntdSabores.push(i);
    }
  }

  adicionarSabor(sabor: Sabor) {
    if (this.saboresSelecionados.length >= this.quantidadeSabores) {
      alert("Maximo de sabores antigido");
      return;
    }

    if (this.saboresSelecionados.includes(sabor)) {
      alert(`${sabor.nome} ja foi selecionado`);
      return;
    }

    this.saboresSelecionados.push(sabor);
  }

  removerSabor(index: number) {
    this.pizza.sabores.splice(index, 1);
  }

  adicionarAoCarrinho() {
    if (this.pizza.sabores.length == 0) {
      alert("Pedido inválido");
      return;
    }

    this.pizza.calcularPreco();
    this.carrinhoService.adicionarNoCarrinho(this.pizza);
  }

  ordernarTamanhosPorQntdFatias() {
    // funcao que orderna os tamanhos pela quantidade de fatias de forma ascendente
    this.tamanhosDB.sort((tamanhoA, tamanhoB) => tamanhoA.qntdFatias - tamanhoB.qntdFatias)
  }
}
