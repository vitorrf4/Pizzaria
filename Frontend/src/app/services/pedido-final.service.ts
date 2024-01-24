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
    return this.http.get<PedidoFinal[]>(`${this.apiUrl}/pedido-final`);
  }

  listarId(id: number) {
    return this.http.get<PedidoFinal>(`${this.apiUrl}/pedido-final/` + id);
  }

  cadastrar(pedidoFinal: PedidoFinal) {
    return this.http.post<PedidoFinal>(`${this.apiUrl}/pedido-final/`, pedidoFinal);
  }

  alterar(pedidoFinal: PedidoFinal) {
    return this.http.put(`${this.apiUrl}/pedido-final/`, pedidoFinal);
  }

}
