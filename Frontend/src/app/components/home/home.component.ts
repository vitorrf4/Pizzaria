import { Component } from '@angular/core';
import {ClienteService} from "../../services/cliente.service";
import {Cliente} from "../../models/Cliente";
import {Router} from "@angular/router";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  cliente : Cliente;

  constructor(private clienteService: ClienteService, private router: Router) {
    this.cliente =  this.clienteService.getClienteLogado();
  }

  deslogar() {
    this.clienteService.deslogarCliente();
    this.router.navigateByUrl("login").then();
  }
}
