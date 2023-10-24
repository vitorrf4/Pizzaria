import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { enviroment } from 'src/enviroments/enviroments';
import { AcompanhamentoPedido } from 'src/models/AcompanhamentoPedido';

@Injectable({
  providedIn: 'root'
})
export class AcompanhamentoPedidoService {
  apiUrl = enviroment.apiUrl

  constructor(private http: HttpClient) { }

  listar(): Observable<AcompanhamentoPedido[]> {
    return this.http.get<AcompanhamentoPedido[]>(`${this.apiUrl}/AcompanhamentoPedido/listar`)
  }

  listarId(id: number): Observable<AcompanhamentoPedido[]> {
    return this.http.get<AcompanhamentoPedido[]>(`${this.apiUrl}/AcompanhamentoPedido/listar/` + id)
  } 

  cadastrar(acompanhamentoPedido: AcompanhamentoPedido): Observable<AcompanhamentoPedido[]>{
    return this.http.post<AcompanhamentoPedido[]>(`${this.apiUrl}/AcompanhamentoPedido/cadastrar/`, acompanhamentoPedido)
  }

  alterar(acompanhamentoPedido: AcompanhamentoPedido): Observable<AcompanhamentoPedido[]>{
    return this.http.put<AcompanhamentoPedido[]>(`${this.apiUrl}/AcompanhamentoPedido/alterar/`, acompanhamentoPedido)
  }
}
