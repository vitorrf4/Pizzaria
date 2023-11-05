import {Component, ElementRef, ViewChild} from '@angular/core';
import {Sabor} from "../../models/Sabor";
import {SaborService} from "../../services/sabor.service";
import {Tamanho} from "../../models/Tamanho";
import {TamanhoService} from "../../services/tamanho.service";
import {PedidoFinal} from "../../models/PedidoFinal";
import {PizzaPedido} from "../../models/PizzaPedido";
import {CarrinhoService} from "../../services/carrinho.service";

@Component({
  selector: 'app-sabor',
  templateUrl: './pizzas.component.html',
  styleUrls: ['./pizzas.component.css']
})
export class PizzasComponent {
  sabores: Sabor[] = [];
  tamanhos : Tamanho[] = [];
  tamanho  = new Tamanho();
  saboresSelecionados : Sabor[] = [];
  @ViewChild("saboresCheckbox") checkboxes! : ElementRef;

  constructor(private saborService: SaborService,
              private tamanhoService: TamanhoService,
              private carrinhoService: CarrinhoService) {
    this.saborService.listar().subscribe(resposta => {
      this.sabores = resposta;
    });

    this.tamanhoService.listar().subscribe(resposta => {
      this.tamanhos = resposta;
      this.ordernarTamanhoPorFatias();
    });
  }

  adicionarSabor(sabor: Sabor) {
    if (this.saboresSelecionados.length >= this.tamanho.maxSabores) {
      alert("Maximo de sabores antigido");
      return;
    }

    this.saboresSelecionados.push(sabor);
  }

  adicionarAoCarrinho() {
    const pizzas = new PizzaPedido(this.saboresSelecionados, this.tamanho, 1);
    this.carrinhoService.adicionarNoCarrinho(pizzas);
  }

  ordernarTamanhoPorFatias() {
    // funcao que orderna os tamanhos pela quantidade de fatias de forma ascendente
    this.tamanhos.sort((tamanhoA, tamanhoB) => tamanhoA.qntdFatias - tamanhoB.qntdFatias)
  }
}
