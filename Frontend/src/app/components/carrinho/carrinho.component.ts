import { Component } from '@angular/core';
import {Pedido} from "../../models/Pedido";
import {CarrinhoService} from "../../services/carrinho.service";
import {AcompanhamentoPedido} from "../../models/AcompanhamentoPedido";

@Component({
  selector: 'app-carrinho',
  templateUrl: './carrinho.component.html',
  styleUrls: ['./carrinho.component.css']
})
export class CarrinhoComponent {
  itensCarrinho: Pedido[] = [];

  constructor(private carrinhoService: CarrinhoService) {
    this.itensCarrinho = this.carrinhoService.itensCarrinho;
  }

  removerDoCarrinho(index: number) {
    this.carrinhoService.removerDoCarrinho(index);
  }
}
