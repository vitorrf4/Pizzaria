import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { enviroment } from 'src/enviroments/enviroments';
import { Endereco } from 'src/app/models/Endereco';

@Injectable({
  providedIn: 'root'
})
export class EnderecoService {

  apiUrl = enviroment.apiUrl

  constructor(private http: HttpClient) { }

  listar(): Observable<Endereco[]>{
    return this.http.get<Endereco[]>(`${this.apiUrl}/Endereco/listar`)
  }

  listarId(id: number): Observable<Endereco[]>{
    return this.http.get<Endereco[]>(`${this.apiUrl}/Endereco/listar/` + id)
  }

  cadastrar(endereco: Endereco): Observable<Endereco[]>{
    return this.http.post<Endereco[]>(`${this.apiUrl}/Endereco/cadastrar/`, endereco)
  }

  alterar(endereco: Endereco): Observable<Endereco[]>{
    return this.http.put<Endereco[]>(`${this.apiUrl}/Endereco/alterar/`, endereco)
  }
}
