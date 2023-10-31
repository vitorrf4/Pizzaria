import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { enviroment } from 'src/enviroments/enviroments';
import { Promocao } from 'src/app/models/Promocao';

@Injectable({
  providedIn: 'root'
})
export class PromocaoService {
  apiUrl = enviroment.apiUrl

  constructor(private http: HttpClient) { }

  listar(): Observable<Promocao[]> {
    return this.http.get<Promocao[]>(`${this.apiUrl}/promocao/listar`)
  }

  listarId(id: number): Observable<Promocao[]> {
    return this.http.get<Promocao[]>(`${this.apiUrl}/promocao/listar/` + id)
  }

  cadastrar(promocao: Promocao): Observable<Promocao[]>{
    return this.http.post<Promocao[]>(`${this.apiUrl}/promocao/cadastrar/`, promocao)
  }

  alterar(promocao: Promocao): Observable<Promocao[]>{
    return this.http.put<Promocao[]>(`${this.apiUrl}/promocao/alterar/`, promocao)
  }
}
