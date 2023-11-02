import { Component } from '@angular/core';
import {Cliente} from "../../models/Cliente";
import {Router} from "@angular/router";
import {LoginService} from "../../services/login.service";
import {CarrinhoService} from "../../services/carrinho.service";
import {BehaviorSubject} from "rxjs";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  cliente : Cliente;
  // O tipo BehaviorSubject liga uma variavel a outra, toda vez que uma variavel mudar
  // a outra atualizara automaticamente, nesse caso quando qualquer item for adicionado
  // ao carrinho, a quantidade mostrada no componente será atualizada
  quantidadeItensCarrinho: BehaviorSubject<number>;

  constructor(private loginService: LoginService,
              private router: Router,
              carrinhoService: CarrinhoService) {
    this.cliente =  this.loginService.getClienteLogado();
    this.quantidadeItensCarrinho = carrinhoService.quantidadeItensCarrinho;
  }

  deslogar() {
    this.loginService.deslogarCliente();
    this.router.navigateByUrl("login").then();
  }
}
