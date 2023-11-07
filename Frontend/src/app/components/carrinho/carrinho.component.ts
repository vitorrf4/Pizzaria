import { Component } from '@angular/core';
import {Pedido} from "../../models/Pedido";
import {CarrinhoService} from "../../services/carrinho.service";
import {PedidoFinal} from "../../models/PedidoFinal";
import {LoginService} from "../../services/login.service";
import {PedidoFinalService} from "../../services/pedido-final.service";

@Component({
  selector: 'app-carrinho',
  templateUrl: './carrinho.component.html',
  styleUrls: ['./carrinho.component.css']
})
export class CarrinhoComponent{
  itensCarrinho : Pedido[];
  pedidoFinal!: PedidoFinal;

  constructor(private carrinhoService: CarrinhoService,
              private clienteService: LoginService,
              private pedidoFinalService: PedidoFinalService) {
    this.itensCarrinho = this.carrinhoService.itensCarrinho;
  }

  removerDoCarrinho(index: number) {
    this.carrinhoService.removerDoCarrinho(index);
    this.itensCarrinho.splice(index, 1);
  }

  finalizarPedido() {
    this.construirPedido();

    this.pedidoFinalService.cadastrar(this.pedidoFinal).subscribe({
      next: () => {
        alert("Pedido finalizado com sucesso");
        this.limparCarrinho();
      },
      error: err => console.log(err)
    });
  }

  construirPedido() {
    const cliente = this.clienteService.clienteLogado;
    const pizzas = this.carrinhoService.filtrarPizzasNoCarrinho();
    const acompanhamentos = this.carrinhoService.filtrarAcompanhamentosNoCarrinho();

    this.pedidoFinal = new PedidoFinal(cliente, pizzas, acompanhamentos);
  }

  limparCarrinho() {
    this.carrinhoService.limparCarrinho();
    this.itensCarrinho = [];
  }
}
