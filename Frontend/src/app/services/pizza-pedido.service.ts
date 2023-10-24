import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { enviroment } from 'src/enviroments/enviroments';
import { PizzaPedido } from 'src/models/PizzaPedido';

@Injectable({
  providedIn: 'root'
})
export class PizzaPedidoService {
  apiUrl = enviroment.apiUrl

  constructor(private http: HttpClient) { }

  listar(): Observable<PizzaPedido[]> {
    return this.http.get<PizzaPedido[]>(`${this.apiUrl}/PizzaPedido/listar`)
  }

  listarId(id: number): Observable<PizzaPedido[]> {
    return this.http.get<PizzaPedido[]>(`${this.apiUrl}/PizzaPedido/listar/` + id)
  } 

  cadastrar(pizzaPedido: PizzaPedido): Observable<PizzaPedido[]>{
    return this.http.post<PizzaPedido[]>(`${this.apiUrl}/PizzaPedido/cadastrar/`, pizzaPedido)
  }

  alterar(pizzaPedido: PizzaPedido): Observable<PizzaPedido[]>{
    return this.http.put<PizzaPedido[]>(`${this.apiUrl}/PizzaPedido/alterar/`, pizzaPedido)
  }
}
