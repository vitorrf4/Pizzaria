import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { enviroment } from 'src/enviroments/enviroments';
import { Cliente } from '../models/Cliente';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  apiUrl = enviroment.apiUrl;

  constructor(private http: HttpClient) { }

  login(email: string, senha: string) {
    return this.http.post<Cliente>(`${this.apiUrl}/auth/login`,{ email, senha });
  }

  cadastro(cliente: Cliente) {
    return this.http.post(`${this.apiUrl}/auth/login`, cliente);
  }
}
