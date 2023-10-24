import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { enviroment } from 'src/enviroments/enviroments';
import { Promocao } from 'src/models/Promocao';

@Injectable({
  providedIn: 'root'
})
export class PromocaoService {
  apiUrl = enviroment.apiUrl

  constructor(private http: HttpClient) { }

  listar(): Observable<Promocao[]> {
    return this.http.get<Promocao[]>(`${this.apiUrl}/Promocao/listar`)
  }

  listarId(id: number): Observable<Promocao[]> {
    return this.http.get<Promocao[]>(`${this.apiUrl}/Promocao/listar/` + id)
  } 

  cadastrar(promocao: Promocao): Observable<Promocao[]>{
    return this.http.post<Promocao[]>(`${this.apiUrl}/Promocao/cadastrar/`, promocao)
  }

  alterar(promocao: Promocao): Observable<Promocao[]>{
    return this.http.put<Promocao[]>(`${this.apiUrl}/Promocao/alterar/`, promocao)
  }
}
