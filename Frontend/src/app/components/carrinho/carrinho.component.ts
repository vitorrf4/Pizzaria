import { Component } from '@angular/core';
import {Pedido} from "../../models/Pedido";
import {CarrinhoService} from "../../services/carrinho.service";
import {AcompanhamentoPedido} from "../../models/AcompanhamentoPedido";
import {PedidoFinal} from "../../models/PedidoFinal";
import {LoginService} from "../../services/login.service";
import {PedidoFinalService} from "../../services/pedido-final.service";
import {PizzaPedido} from "../../models/PizzaPedido";
import {AcompanhamentoPedidoService} from "../../services/acompanhamento-pedido.service";
import {PizzaPedidoService} from "../../services/pizza-pedido.service";

@Component({
  selector: 'app-carrinho',
  templateUrl: './carrinho.component.html',
  styleUrls: ['./carrinho.component.css']
})
export class CarrinhoComponent{
  itensCarrinho : Pedido[];
  pedidoFinal = new PedidoFinal();

  constructor(private carrinhoService: CarrinhoService,
              private clienteService: LoginService,
              private pedidoFinalService: PedidoFinalService) {
    this.itensCarrinho = this.carrinhoService.itensCarrinho;

    this.filtrarPedidos();
  }

  filtrarPedidos() {
    this.itensCarrinho.forEach(pedido => {
      if (pedido instanceof PizzaPedido) {
        this.pedidoFinal.pizzas.push(pedido);
      }
      if (pedido instanceof  AcompanhamentoPedido) {
        this.pedidoFinal.acompanhamentos.push(pedido);
      }
    });
  }

  // fix: item é removido do carrinho mas não do pedidoFinal
  removerDoCarrinho(index: number) {
    this.carrinhoService.removerDoCarrinho(index);
    this.itensCarrinho.splice(index, 1);
  }

  finalizarPedido() {
    this.pedidoFinal.cliente = this.clienteService.clienteLogado;
    console.log(this.pedidoFinal);

    this.pedidoFinalService.cadastrar(this.pedidoFinal).subscribe({
      next: res => {
        alert("Pedido finalizado");
        this.limparCarrinho();
      },
      error: err => console.log(err)
    });
  }

  limparCarrinho() {
    this.carrinhoService.limparCarrinho();
  }
}
