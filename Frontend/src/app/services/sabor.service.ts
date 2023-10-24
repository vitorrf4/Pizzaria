import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { enviroment } from 'src/enviroments/enviroments';
import { Sabor } from 'src/app/models/Sabor';

@Injectable({
  providedIn: 'root'
})
export class SaborService {
  apiUrl = enviroment.apiUrl

  constructor(private http: HttpClient) { }

  listar(): Observable<Sabor[]> {
    return this.http.get<Sabor[]>(`${this.apiUrl}/Sabor/listar`)
  }

  listarId(id: number): Observable<Sabor[]> {
    return this.http.get<Sabor[]>(`${this.apiUrl}/Sabor/listar/` + id)
  }

  cadastrar(sabor: Sabor): Observable<Sabor[]>{
    return this.http.post<Sabor[]>(`${this.apiUrl}/Sabor/cadastrar/`, sabor)
  }

  alterar(sabor: Sabor): Observable<Sabor[]>{
    return this.http.put<Sabor[]>(`${this.apiUrl}/Sabor/alterar/`, sabor)
  }
}
