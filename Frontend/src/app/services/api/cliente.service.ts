import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { Cliente } from 'src/app/models/entities/Cliente';
import { enviroment } from 'src/enviroments/enviroments';
import {PedidoFinal} from "../../models/entities/PedidoFinal";

@Injectable({
  providedIn: 'root'
})
export class ClienteService {
  apiUrl = enviroment.apiUrl;

  constructor(private http: HttpClient) { }

  listar(): Observable<Cliente[]> {
    return this.http.get<Cliente[]>(`${this.apiUrl}/cliente`);
  }

  listarCpf(id: number) {
    return this.http.get<Cliente>(`${this.apiUrl}/cliente/` + id);
  }

  cadastrar(cliente: Cliente) {
    return this.http.post<Cliente>(`${this.apiUrl}/cliente/`, cliente);
  }

  alterar(cliente: Cliente) {
    return this.http.put(`${this.apiUrl}/cliente/`, cliente);
  }

  listarPedidosPorCliente(id: number) {
    return this.http.get<PedidoFinal[]>(`${this.apiUrl}/cliente/${id}/pedidos`);
  }
}
