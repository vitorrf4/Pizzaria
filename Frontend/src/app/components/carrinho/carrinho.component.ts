import { Component, OnInit } from '@angular/core';
import {Pedido} from "../../models/Pedido";
import {CarrinhoService} from "../../services/carrinho.service";
import {PedidoFinal} from "../../models/PedidoFinal";
import {LoginService} from "../../services/login.service";
import {PedidoFinalService} from "../../services/pedido-final.service";
import { Router} from "@angular/router";

@Component({
  selector: 'app-carrinho',
  templateUrl: './carrinho.component.html',
  styleUrls: ['./carrinho.component.css']
})
export class CarrinhoComponent implements OnInit{
  itensCarrinho : Pedido[] = [];
  pedidoFinal: PedidoFinal = new PedidoFinal();

  constructor(private router : Router,
              private carrinhoService: CarrinhoService,
              private clienteService: LoginService,
              private pedidoFinalService: PedidoFinalService,) {
  }

  ngOnInit() {
    this.itensCarrinho = this.carrinhoService.itensCarrinho;
    this.construirPedido();
  }

  removerDoCarrinho(index: number) {
    this.carrinhoService.removerDoCarrinho(index);
    this.construirPedido();
  }

  construirPedido() {
    if (this.itensCarrinho.length <= 0) {
      return;
    }

    const cliente = this.clienteService.getClienteLogado();
    const pizzas = this.carrinhoService.filtrarPizzasNoCarrinho();
    const acompanhamentos = this.carrinhoService.filtrarAcompanhamentosNoCarrinho();

    this.pedidoFinal = new PedidoFinal(cliente.cpf, cliente.endereco, pizzas, acompanhamentos);
  }
  
  finalizarPedido() {
    this.construirPedido();
    if (this.pedidoFinal.pizzas.length <= 0) {
      alert("Inclua pelo menos uma pizza no seu pedido");
      return;
    }

    this.salvarPedidoFinal();
  }

  salvarPedidoFinal() {
    this.pedidoFinalService.cadastrar(this.pedidoFinal).subscribe({
      next: () => {
        alert("Pedido finalizado com sucesso");
        this.limparCarrinho();
        this.router.navigateByUrl("home/pedidos").then();
      },
      error: err => console.log(err)
    });
  }

  limparCarrinho() {
    this.carrinhoService.limparCarrinho();
    this.itensCarrinho = [];
  }
}
