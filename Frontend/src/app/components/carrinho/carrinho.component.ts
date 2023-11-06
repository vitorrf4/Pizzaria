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
              private pedidoFinalService: PedidoFinalService,
              private pizzaPedidoService: PizzaPedidoService,
              private acompanhamentoPedidoService : AcompanhamentoPedidoService) {
    this.itensCarrinho = this.carrinhoService.itensCarrinho;
    
    this.filtrarPedidos();
    this.cadastrarAcompanhamento();
    this.cadastrarPizza();
  }

  removerDoCarrinho(index: number) {
    this.carrinhoService.removerDoCarrinho(index);
  }

  finalizarPedido() {
    this.pedidoFinal = new PedidoFinal();
    this.pedidoFinal.cliente = this.clienteService.clienteLogado;

    this.pedidoFinalService.cadastrar(this.pedidoFinal).subscribe({
      next: () => alert("Pedido finalizado"),
      error: err => console.log(err)
    });
  }

  cadastrarFinal() {
    console.log("final:");
    console.log(this.pedidoFinal);

  }

  cadastrarPizza() {
    console.log("pizzas:");
    this.pedidoFinal.pizzas.forEach(pizza => {
      this.pizzaPedidoService.cadastrar(pizza).subscribe(res => {
        pizza = res;
      });
    });
  }

  cadastrarAcompanhamento() {
    console.log("acompanhamentos");
    this.pedidoFinal.acompanhamentos.forEach(acomp => {
      this.acompanhamentoPedidoService.cadastrar(acomp).subscribe(res => {
        acomp = res;
      });
    });

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
}
