import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { enviroment } from 'src/enviroments/enviroments';
import { Acompanhamento } from 'src/app/models/Acompanhamento';

@Injectable({
  providedIn: 'root'
})
export class AcompanhamentoService {
  apiUrl = enviroment.apiUrl

  constructor(private http: HttpClient) { }

  listar(): Observable<Acompanhamento[]> {
    return this.http.get<Acompanhamento[]>(`${this.apiUrl}/Acompanhamento/listar`)
  }

  listarId(id: number): Observable<Acompanhamento[]> {
    return this.http.get<Acompanhamento[]>(`${this.apiUrl}/Acompanhamento/listar/` + id)
  }

  cadastrar(acompanhamento: Acompanhamento): Observable<Acompanhamento[]>{
    return this.http.post<Acompanhamento[]>(`${this.apiUrl}/Acompanhamento/cadastrar/`, acompanhamento)
  }

  alterar(acompanhamento: Acompanhamento): Observable<Acompanhamento[]>{
    return this.http.put<Acompanhamento[]>(`${this.apiUrl}/Acompanhamento/alterar/`, acompanhamento)
  }
}
