import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class CepService {
  constructor(private http: HttpClient) { }

  buscarCep(cep: String) {
    return this.http.get<any>(`https://viacep.com.br/ws/${cep}/json`);
  }
}
