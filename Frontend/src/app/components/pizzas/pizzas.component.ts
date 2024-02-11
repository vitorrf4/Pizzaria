import {Component, OnInit} from '@angular/core';
import {Sabor} from "../../models/entities/Sabor";
import {SaborService} from "../../services/api/sabor.service";
import {Tamanho} from "../../models/entities/Tamanho";
import {TamanhoService} from "../../services/api/tamanho.service";
import {CarrinhoService} from "../../services/carrinho.service";
import {PizzaBuilderService} from "../../services/pizza-builder.service";

@Component({
  selector: 'app-sabor',
  templateUrl: './pizzas.component.html',
  styleUrls: ['./pizzas.component.css']
})
export class PizzasComponent implements OnInit {
  sabores: Sabor[] = [];
  tamanhos: Tamanho[] = [];

  constructor(private saborService: SaborService,
              private tamanhoService: TamanhoService,
              private pizzaBuilder: PizzaBuilderService,
              private carrinhoService: CarrinhoService) { }

  ngOnInit() {
    this.saborService.listar().subscribe(resposta => {
      this.sabores = resposta;
      this.sabores.sort((saborA, saborB) => saborA.preco - saborB.preco);
    });

    this.tamanhoService.listar().subscribe(resposta => {
      this.tamanhos = resposta;
      this.tamanhos.sort((tamanhoA, tamanhoB) => tamanhoA.qntdFatias - tamanhoB.qntdFatias);

      this.pizzaBuilder.mudarTamanho(this.tamanhos[0]);
    });
  }

  get pizza() {
    return this.pizzaBuilder.pizza;
  }

  get maxQntdSabores() {
    return Array.from(Array(this.pizza.tamanho.maxSabores).keys()).map(x => x + 1);
  }

  adicionarSabor(sabor: Sabor) {
    const resultado = this.pizzaBuilder.adicionarSabor(sabor);
    if (resultado.temErros) {
      alert(resultado.mensagemErro);
    }
  }

  removerSabor(index: number) {
    this.pizzaBuilder.removerSabor(index);
  }

  mudarTamanho(tamanho: string) {
    //TODO fix select de tamanho não volta pra broto após adicionar pizza
    const tamanhoDb = this.tamanhos.find(t => t.nome == tamanho) ?? this.tamanhos[0];
    this.pizzaBuilder.mudarTamanho(tamanhoDb);
  }

  mudarQuantidadePizzas(quantidade: number) {
    this.pizzaBuilder.mudarQuantidadePizzas(quantidade);
  }

  mudarQuantidadeSabores(quantidade: string) {
    const a = Number.parseFloat(quantidade);
    this.pizzaBuilder.mudarQuantidadeSabores(a);
  }

  adicionarAoCarrinho() {
    const resultadoBuild = this.pizzaBuilder.pizzaEstaValida();
    if (resultadoBuild.temErros) {
      return alert(resultadoBuild.mensagemErro);
    }

    const pizza = this.pizzaBuilder.buildPizza();
    this.carrinhoService.adicionarNoCarrinho(pizza);

    alert("Pizza adicionada!");

    this.pizzaBuilder.resetPizza();
  }
}
