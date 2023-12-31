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
    return this.http.get<Acompanhamento[]>(`${this.apiUrl}/acompanhamento/listar`)
  }

  listarId(id: number) {
    return this.http.get<Acompanhamento>(`${this.apiUrl}/acompanhamento/listar/` + id)
  }

  cadastrar(acompanhamento: Acompanhamento) {
    return this.http.post<Acompanhamento>(`${this.apiUrl}/acompanhamento/cadastrar/`, acompanhamento)
  }

  alterar(acompanhamento: Acompanhamento) {
    return this.http.put(`${this.apiUrl}/acompanhamento/alterar/`, acompanhamento)
  }
}
