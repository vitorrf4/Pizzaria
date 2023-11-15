import { Component } from '@angular/core';
import {Pedido} from "../../models/Pedido";
import {CarrinhoService} from "../../services/carrinho.service";
import {PedidoFinal} from "../../models/PedidoFinal";
import {LoginService} from "../../services/login.service";
import {PedidoFinalService} from "../../services/pedido-final.service";
import { PizzaPedidoService } from 'src/app/services/pizza-pedido.service';
import { AcompanhamentoPedidoService } from 'src/app/services/acompanhamento-pedido.service';
import { PizzaPedido } from 'src/app/models/PizzaPedido';

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
              private pizzaPedidoService: PizzaPedidoService,
              private acompService: AcompanhamentoPedidoService,
              private pedidoFinalService: PedidoFinalService,) {
    this.itensCarrinho = this.carrinhoService.itensCarrinho;
  }

  removerDoCarrinho(index: number) {
    this.carrinhoService.removerDoCarrinho(index);
    this.itensCarrinho.splice(index, 1);
  }

  finalizarPedido() {
    this.construirPedido();

    for (let i = 0; i < this.pedidoFinal.pizzas.length; i++) {
      let pizza = this.pedidoFinal.pizzas[i];
      this.pizzaPedidoService.cadastrar(pizza).subscribe(res => {
        this.pedidoFinal.pizzas[i] = res;
      });
    }

    setTimeout(() => {
      console.log(this.pedidoFinal.pizzas);

      this.pedidoFinalService.cadastrar(this.pedidoFinal).subscribe({
        next: res => {
          // alert("Pedido finalizado com sucesso");
          // this.limparCarrinho();
          console.log(res.id + " criado");
        },
        error: err => console.log(err)
      });

    }, 900);

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
