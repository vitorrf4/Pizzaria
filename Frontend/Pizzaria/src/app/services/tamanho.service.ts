import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { enviroment } from 'src/enviroments/enviroments';
import { Tamanho } from 'src/models/Tamanho';

@Injectable({
  providedIn: 'root'
})
export class TamanhoService {
  apiUrl = enviroment.apiUrl

  constructor(private http: HttpClient) { }

  listar(): Observable<Tamanho[]> {
    return this.http.get<Tamanho[]>(`${this.apiUrl}/Tamanho/listar`)
  }

  listarId(nome: string): Observable<Tamanho[]> {
    return this.http.get<Tamanho[]>(`${this.apiUrl}/Tamanho/listar/` + nome)
  } 

  cadastrar(tamanho: Tamanho): Observable<Tamanho[]>{
    return this.http.post<Tamanho[]>(`${this.apiUrl}/Tamanho/cadastrar/`, tamanho)
  }

  alterar(tamanho: Tamanho): Observable<Tamanho[]>{
    return this.http.put<Tamanho[]>(`${this.apiUrl}/Tamanho/alterar/`, tamanho)
  }
}
