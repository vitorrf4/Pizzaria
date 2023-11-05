import { Component } from '@angular/core';
import {ClienteService} from "../../services/cliente.service";
import {Router} from "@angular/router";
import {FormControl, FormGroup} from "@angular/forms";
import {LoginService} from "../../services/login.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  formularioCliente: any;

  constructor(private clienteService: ClienteService, private router: Router,
              private loginService: LoginService) {
    this.formularioCliente = new FormGroup({
      cpf: new FormControl(null),
    });
  }

  logarCliente() {
    const cpf = this.formularioCliente.value.cpf;

    // Busca o cpf no banco de dados e caso exista, salva o cliente
    // na sessão, e navega para a página "home"
    this.clienteService.listarCpf(cpf).subscribe({
      next: cliente => {
        this.loginService.salvarClienteLogado(cliente);
        this.router.navigateByUrl("/home").then();
      },
      error: err => console.log(err)
    });
  }

}
