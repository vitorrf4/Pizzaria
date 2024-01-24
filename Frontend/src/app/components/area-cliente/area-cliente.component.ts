import {Component} from '@angular/core';
import {ClienteService} from "../../services/cliente.service";
import {Cliente} from "../../models/Cliente";
import {LoginService} from "../../services/login.service";

@Component({
  selector: 'app-cliente',
  templateUrl: './area-cliente.component.html',
  styleUrls: ['./area-cliente.component.css']
})
export class AreaClienteComponent {
  cliente: Cliente = new Cliente();

  constructor(private clienteService: ClienteService, loginService: LoginService) {
    const cpf = loginService.getClienteLogado().cpf;

    this.clienteService.listarCpf(cpf).subscribe(resposta => {
      this.cliente = resposta;
    });
  }

}
