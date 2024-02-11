import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { enviroment } from 'src/enviroments/enviroments';
import { Endereco } from 'src/app/models/entities/Endereco';

@Injectable({
  providedIn: 'root'
})
export class EnderecoService {

  apiUrl = enviroment.apiUrl

  constructor(private http: HttpClient) { }

  listar(): Observable<Endereco[]>{
    return this.http.get<Endereco[]>(`${this.apiUrl}/endereco`);
  }

  listarId(id: number) {
    return this.http.get<Endereco>(`${this.apiUrl}/endereco/` + id);
  }

  cadastrar(endereco: Endereco) {
    return this.http.post<Endereco>(`${this.apiUrl}/endereco/`, endereco);
  }

  alterar(endereco: Endereco) {
    return this.http.put(`${this.apiUrl}/endereco/`, endereco);
  }
}
