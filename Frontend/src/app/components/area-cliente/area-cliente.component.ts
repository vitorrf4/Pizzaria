import {Component, OnInit} from '@angular/core';
import {ClienteService} from "../../services/cliente.service";
import {Cliente} from "../../models/Cliente";
import {LoginService} from "../../services/login.service";

@Component({
  selector: 'app-cliente',
  templateUrl: './area-cliente.component.html',
  styleUrls: ['./area-cliente.component.css']
})
export class AreaClienteComponent implements OnInit{
  cliente: Cliente = new Cliente();

  constructor(private clienteService: ClienteService, 
              private loginService: LoginService) {}

  ngOnInit() {
    const cpf = this.loginService.getClienteLogado().id;

    this.clienteService.listarCpf(cpf).subscribe(resposta => {
      this.cliente = resposta;
    });
  }

}
