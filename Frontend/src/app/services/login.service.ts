import { Injectable } from '@angular/core';
import {Cliente} from "../models/entities/Cliente";

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private clienteLogado;
  estaLogado = false;

  constructor() {
    this.clienteLogado = JSON.parse(sessionStorage.getItem("cliente")!);
    if (this.clienteLogado != null) {
      this.estaLogado = true;
    }
  }

  getClienteLogado() : Cliente {
    return this.clienteLogado;
  }

  salvarClienteLogado(cliente: Cliente) {
    this.clienteLogado = cliente;
    sessionStorage.setItem("cliente", JSON.stringify(cliente));
    this.estaLogado = true;
  }

  deslogarCliente() {
    this.clienteLogado = new Cliente();
    sessionStorage.removeItem("cliente");
    this.estaLogado = false;
  }
}
