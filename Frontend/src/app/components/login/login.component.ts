import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";
import {FormControl, FormGroup} from "@angular/forms";
import {LoginService} from "../../services/login.service";
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{
  formularioCliente!: FormGroup;

  constructor(private authService: AuthService, 
              private router: Router,
              private loginService: LoginService) { }

  ngOnInit() {
    this.formularioCliente = new FormGroup({
      email: new FormControl(),
      senha: new FormControl(),
    });
  }

  logarCliente() {
    const email = this.formularioCliente.value.email;
    const senha = this.formularioCliente.value.senha;

    if (!email || !senha) {
      alert("Cpf está vazio");
      return;
    }
    
    this.authService.login(email, senha).subscribe({
      next: async cliente => {
        this.loginService.salvarClienteLogado(cliente);
        await this.router.navigateByUrl("/home/area-cliente");
      },
      error: err => {
        console.log(err);
        alert("Login inválido");
      }
    });
  }
}
