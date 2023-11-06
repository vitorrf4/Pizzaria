import { Component } from '@angular/core';
import { PedidoFinal } from 'src/app/models/PedidoFinal';
import { PizzaPedido } from 'src/app/models/PizzaPedido';
import { LoginService } from 'src/app/services/login.service';
import { PedidoFinalService } from 'src/app/services/pedido-final.service';

@Component({
  selector: 'app-pedidos',
  templateUrl: './pedidos.component.html',
  styleUrls: ['./pedidos.component.css']
})
export class PedidosComponent {
  meusPedidos : PedidoFinal[] = [];
  descricoes: string[] = [];

  constructor(private pedidoFinalService: PedidoFinalService, private loginService: LoginService) { 
    this.consultarMeusPedidos();
  }

  consultarMeusPedidos() {
    const cliente = this.loginService.clienteLogado;

    this.pedidoFinalService.listarPedidosPorCliente(cliente.cpf).subscribe(resposta => {
      this.meusPedidos = resposta;
      this.getDescricoes();
    });
  }

  getDescricoes() {
    this.meusPedidos.forEach(pedido => {
      pedido.pizzas.forEach(pizza => {
        this.getDescricaoPizza(pizza);
      });
    });
  }

  getDescricaoPizza(pizza: PizzaPedido) {
    const tamanho = `Pizza ${pizza.tamanho.nome} de `;
    let nomesSabores = "";

    for (let i = 0; i < pizza.sabores.length; i++) {
      if (i == pizza.sabores.length - 1) {
        nomesSabores = nomesSabores + pizza.sabores[i].nome;
        break;
      }
      nomesSabores = nomesSabores + pizza.sabores[i].nome + ", ";
    }

    this.descricoes.push(tamanho + nomesSabores);
  }
}
