import { Component } from '@angular/core';
import {Cliente} from "../../models/Cliente";
import {Router} from "@angular/router";
import {LoginService} from "../../services/login.service";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  cliente : Cliente;

  constructor(private loginService: LoginService, private router: Router) {
    this.cliente =  this.loginService.getClienteLogado();
  }

  deslogar() {
    this.loginService.deslogarCliente();
    this.router.navigateByUrl("login").then();
  }
}
