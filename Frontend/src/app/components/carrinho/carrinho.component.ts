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
export class CarrinhoComponent implements OnInit {
  itensCarrinho: Pedido[] = [];
  pedidoFinal: PedidoFinal | null = null;

  constructor(private router : Router,
              private carrinhoService: CarrinhoService,
              private clienteService: LoginService,
              private pedidoFinalService: PedidoFinalService) { }

  ngOnInit() {
   this.carrinhoService.itensCarrinho$.subscribe(i => {
      this.itensCarrinho = i;
      this.construirPedido();
    });
  }

  construirPedido() {
    const cliente = this.clienteService.getClienteLogado();
    const pizzas = this.carrinhoService.filtrarPizzasNoCarrinho();
    const acompanhamentos = this.carrinhoService.filtrarAcompanhamentosNoCarrinho();

    this.pedidoFinal = new PedidoFinal(cliente.id, cliente.endereco, pizzas, acompanhamentos);
  }
 
  removerDoCarrinho(index: number) {
    this.carrinhoService.removerDoCarrinho(index);
  }

  finalizarPedido() {
    if (!this.pedidoFinal || this.pedidoFinal.pizzas.length <= 0) {
      alert("Inclua pelo menos uma pizza no seu pedido");
      return;
    }

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
  }
}
