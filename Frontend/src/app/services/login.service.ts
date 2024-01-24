import { Injectable } from '@angular/core';
import {Cliente} from "../models/Cliente";

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  clienteLogado: Cliente;

  constructor() {
    this.clienteLogado = JSON.parse(sessionStorage.getItem("cliente")!);
  }

  getClienteLogado(): Cliente {
    return this.clienteLogado;
  }

  salvarClienteLogado(cliente: Cliente) {
    this.clienteLogado = cliente;
    sessionStorage.setItem("cliente", JSON.stringify(cliente));
  }

  deslogarCliente() {
    this.clienteLogado = new Cliente();
    sessionStorage.removeItem("cliente");
  }
}
