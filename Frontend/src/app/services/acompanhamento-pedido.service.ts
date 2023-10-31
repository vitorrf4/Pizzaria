import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { enviroment } from 'src/enviroments/enviroments';
import { AcompanhamentoPedido } from 'src/app/models/AcompanhamentoPedido';

@Injectable({
  providedIn: 'root'
})
export class AcompanhamentoPedidoService {
  apiUrl = enviroment.apiUrl

  constructor(private http: HttpClient) { }

  listar(): Observable<AcompanhamentoPedido[]> {
    return this.http.get<AcompanhamentoPedido[]>(`${this.apiUrl}/acompanhamentoPedido/listar`)
  }

  listarId(id: number) {
    return this.http.get<AcompanhamentoPedido>(`${this.apiUrl}/acompanhamentoPedido/listar/` + id)
  }

  cadastrar(acompanhamentoPedido: AcompanhamentoPedido) {
    return this.http.post<AcompanhamentoPedido>(`${this.apiUrl}/acompanhamentoPedido/cadastrar/`, acompanhamentoPedido)
  }

  alterar(acompanhamentoPedido: AcompanhamentoPedido) {
    return this.http.put(`${this.apiUrl}/acompanhamentoPedido/alterar/`, acompanhamentoPedido)
  }
}
