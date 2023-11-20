import { Component } from '@angular/core';
import {PedidoFinal} from "../../models/PedidoFinal";
import {ClienteService} from "../../services/cliente.service";
import {LoginService} from "../../services/login.service";
import {PizzaPedido} from "../../models/PizzaPedido";
import {AcompanhamentoPedido} from "../../models/AcompanhamentoPedido";

@Component({
  selector: 'app-pedidos',
  templateUrl: './pedidos.component.html',
  styleUrls: ['./pedidos.component.css']
})
export class PedidosComponent {
  pedidos: PedidoFinal[] = [];


  constructor(private clienteService: ClienteService, loginService: LoginService) {
    const cpf = loginService.clienteLogado.cpf;

    clienteService.listarPedidosPorCliente(cpf).subscribe(resposta => {
      this.pedidos = resposta;
      this.ordernarPedidoPorData();
    })
  }

  getDescricaoPizza(pizza: PizzaPedido): string {
    pizza = new PizzaPedido(pizza.sabores, pizza.tamanho, pizza.quantidade);

    return pizza.getDescricao();
  }

  getDescricaoAcompanhamento(acomp : AcompanhamentoPedido): string {
    acomp = new AcompanhamentoPedido(acomp.acompanhamento, acomp.quantidade);

    return acomp.getDescricao();
  }

  ordernarPedidoPorData() {
    this.pedidos.sort((a, b) => b.id - a.id);
  }
}
