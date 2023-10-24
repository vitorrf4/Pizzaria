import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Observable } from 'rxjs';
import { Cliente } from 'src/models/Cliente';
import { enviroment } from 'src/enviroments/enviroments';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type' : 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class ClienteService {

  apiUrl = enviroment.apiUrl

  constructor(private http: HttpClient) { }

  listar(): Observable<Cliente[]> {
    return this.http.get<Cliente[]>(`${this.apiUrl}/Cliente/listar`);
  }

  listarCpf(cpf: string): Observable<Cliente[]> {
    return this.http.get<Cliente[]>(`${this.apiUrl}/Cliente/listar/` + cpf);
  }

  cadastrar(cliente: Cliente): Observable<Cliente[]> {
    return this.http.post<Cliente[]>(`${this.apiUrl}/Cliente/cadastrar/`, cliente);
  }

  alterar(cliente: Cliente): Observable<Cliente[]> {
    return this.http.put<Cliente[]>(`${this.apiUrl}/Cliente/alterar/`, cliente);
  }
}
