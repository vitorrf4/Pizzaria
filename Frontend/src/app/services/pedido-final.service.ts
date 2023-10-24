import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { enviroment } from 'src/enviroments/enviroments';
import { PedidoFinal } from 'src/app/models/PedidoFinal';

@Injectable({
  providedIn: 'root'
})
export class PedidoFinalService {
  apiUrl = enviroment.apiUrl

  constructor(private http: HttpClient) { }

  listar(): Observable<PedidoFinal[]> {
    return this.http.get<PedidoFinal[]>(`${this.apiUrl}/PedidoFinal/listar`)
  }

  listarId(id: number): Observable<PedidoFinal[]> {
    return this.http.get<PedidoFinal[]>(`${this.apiUrl}/PedidoFinal/listar/` + id)
  }

  cadastrar(pedidoFinal: PedidoFinal): Observable<PedidoFinal[]>{
    return this.http.post<PedidoFinal[]>(`${this.apiUrl}/PedidoFinal/cadastrar/`, pedidoFinal)
  }

  alterar(pedidoFinal: PedidoFinal): Observable<PedidoFinal[]>{
    return this.http.put<PedidoFinal[]>(`${this.apiUrl}/PedidoFinal/alterar/`, pedidoFinal)
  }
}
