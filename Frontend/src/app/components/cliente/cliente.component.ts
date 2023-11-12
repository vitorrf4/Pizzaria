import {Component} from '@angular/core';
import {ClienteService} from "../../services/cliente.service";
import {Cliente} from "../../models/Cliente";
import {LoginService} from "../../services/login.service";
import {PedidoFinal} from "../../models/PedidoFinal";

@Component({
  selector: 'app-cliente',
  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.css']
})
export class ClienteComponent {
  cliente: Cliente = new Cliente();
  pedidos: PedidoFinal[] = [];

  constructor(private clienteService: ClienteService, loginService: LoginService) {
    const cpf = loginService.clienteLogado.cpf;

    this.clienteService.listarCpf(cpf).subscribe(resposta => {
      this.cliente = resposta;
    });

    clienteService.listarPedidosPorCliente(cpf).subscribe(resposta => {
      this.pedidos = resposta;
    })

  }

}
