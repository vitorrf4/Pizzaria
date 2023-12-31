import {Component} from '@angular/core';
import {ClienteService} from "../../services/cliente.service";
import {Cliente} from "../../models/Cliente";
import {LoginService} from "../../services/login.service";
import {PedidoFinal} from "../../models/PedidoFinal";
import {PizzaPedido} from 'src/app/models/PizzaPedido';
import {AcompanhamentoPedido} from 'src/app/models/AcompanhamentoPedido';

@Component({
  selector: 'app-cliente',
  templateUrl: './area-cliente.component.html',
  styleUrls: ['./area-cliente.component.css']
})
export class AreaClienteComponent {
  cliente: Cliente = new Cliente();

  constructor(private clienteService: ClienteService, loginService: LoginService) {
    const cpf = loginService.clienteLogado.cpf;

    this.clienteService.listarCpf(cpf).subscribe(resposta => {
      this.cliente = resposta;
    });
  }

}
