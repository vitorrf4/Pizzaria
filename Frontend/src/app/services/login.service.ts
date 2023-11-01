import { Injectable } from '@angular/core';
import {Cliente} from "../models/Cliente";

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  clienteLogado: Cliente;

  constructor() {
    // Quando o programa é iniciado, ele pega o valor "cliente" salvo na sessão do browser, transforma
    // para um JSON e o atribue para a variavel clienteLogado que pode ser usado
    // pelos outros componentes.
    // O "!" indica para o angular que o valor não será null ou undefined, sem ele
    // o compilador reclama dessa atribuição
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
