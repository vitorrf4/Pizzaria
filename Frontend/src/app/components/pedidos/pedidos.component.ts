import { Component } from '@angular/core';
import { PedidoFinal } from 'src/app/models/PedidoFinal';
import { LoginService } from 'src/app/services/login.service';
import { PedidoFinalService } from 'src/app/services/pedido-final.service';

@Component({
  selector: 'app-pedidos',
  templateUrl: './pedidos.component.html',
  styleUrls: ['./pedidos.component.css']
})
export class PedidosComponent {
  meusPedidos : PedidoFinal[] = [];

  constructor(private pedidoFinalService: PedidoFinalService, private loginService: LoginService) {
    this.consultarMeusPedidos();
  }

  consultarMeusPedidos() {
    const cliente = this.loginService.clienteLogado;

    this.pedidoFinalService.listarPedidosPorCliente(cliente.cpf).subscribe(resposta => {
      this.meusPedidos = resposta;
    });
  }

}
