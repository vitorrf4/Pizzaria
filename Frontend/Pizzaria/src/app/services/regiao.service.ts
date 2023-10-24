import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { enviroment } from 'src/enviroments/enviroments';
import { Regiao } from 'src/models/Regiao';

@Injectable({
  providedIn: 'root'
})
export class RegiaoService {
  apiUrl = enviroment.apiUrl

  constructor(private http: HttpClient) { }

  listar(): Observable<Regiao[]> {
    return this.http.get<Regiao[]>(`${this.apiUrl}/Regiao/listar`)
  }

  listarId(id: number): Observable<Regiao[]> {
    return this.http.get<Regiao[]>(`${this.apiUrl}/Regiao/listar/` + id)
  } 

  cadastrar(regiao: Regiao): Observable<Regiao[]>{
    return this.http.post<Regiao[]>(`${this.apiUrl}/Regiao/cadastrar/`, regiao)
  }

  alterar(regiao: Regiao): Observable<Regiao[]>{
    return this.http.put<Regiao[]>(`${this.apiUrl}/Regiao/alterar/`, regiao)
  }


}
