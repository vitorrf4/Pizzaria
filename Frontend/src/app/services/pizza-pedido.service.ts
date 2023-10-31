import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { enviroment } from 'src/enviroments/enviroments';
import { PizzaPedido } from 'src/app/models/PizzaPedido';

@Injectable({
  providedIn: 'root'
})
export class PizzaPedidoService {
  apiUrl = enviroment.apiUrl

  constructor(private http: HttpClient) { }

  listar(): Observable<PizzaPedido[]> {
    return this.http.get<PizzaPedido[]>(`${this.apiUrl}/pizzaPedido/listar`)
  }

  listarId(id: number) {
    return this.http.get<PizzaPedido>(`${this.apiUrl}/pizzaPedido/listar/` + id)
  }

  cadastrar(pizzaPedido: PizzaPedido) {
    return this.http.post<PizzaPedido>(`${this.apiUrl}/pizzaPedido/cadastrar/`, pizzaPedido)
  }

  alterar(pizzaPedido: PizzaPedido) {
    return this.http.put(`${this.apiUrl}/pizzaPedido/alterar/`, pizzaPedido)
  }
}
