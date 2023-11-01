import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Observable } from 'rxjs';
import { Cliente } from 'src/app/models/Cliente';
import { enviroment } from 'src/enviroments/enviroments';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {
  apiUrl = enviroment.apiUrl;
  clienteLogado: Cliente;

  constructor(private http: HttpClient) {
    // Quando o programa é iniciado, ele pega o valor "cliente" salvo na sessão do browser, transforma
    // para um JSON e o atribue para a variavel clienteLogado que pode ser usado
    // pelos outros componentes.
    // O "!" indica para o angular que o valor não será null ou undefined, sem ele
    // o compilador reclama dessa atribuição
    this.clienteLogado = JSON.parse(sessionStorage.getItem("cliente")!);
  }

  listar(): Observable<Cliente[]> {
    return this.http.get<Cliente[]>(`${this.apiUrl}/cliente/listar`);
  }

  listarCpf(cpf: string) {
    return this.http.get<Cliente>(`${this.apiUrl}/cliente/listar/` + cpf);
  }

  cadastrar(cliente: Cliente) {
    return this.http.post<Cliente[]>(`${this.apiUrl}/cliente/cadastrar/`, cliente);
  }

  alterar(cliente: Cliente) {
    return this.http.put(`${this.apiUrl}/cliente/alterar/`, cliente);
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
