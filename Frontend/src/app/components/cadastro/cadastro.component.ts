import {Component, OnInit} from '@angular/core';
import {Cliente} from "../../models/Cliente";
import {Regiao} from "../../models/Regiao";
import {ClienteService} from "../../services/cliente.service";
import {RegiaoService} from "../../services/regiao.service";
import {FormControl, FormGroup} from "@angular/forms";
import {Router} from "@angular/router";
import {LoginService} from "../../services/login.service";

@Component({
  selector: 'app-cadastro',
  templateUrl: './cadastro.component.html',
  styleUrls: ['./cadastro.component.css']
})
export class CadastroComponent implements OnInit {
  formularioCliente: any;
  formularioEndereco: any;
  regioes: Regiao[] = [];

  constructor(private clienteService: ClienteService,
              private regiaoService: RegiaoService,
              private router: Router,
              private loginService: LoginService) {
    this.formularioCliente = new FormGroup({
      cpf: new FormControl(null),
      nome: new FormControl(null),
      telefone: new FormControl(null),
      dataAniversario: new FormControl(null)
    })

    this.formularioEndereco = new FormGroup({
      rua: new FormControl(null),
      numero: new FormControl(null),
      cep: new FormControl(null),
      complemento: new FormControl(null),
      regiao: new FormControl(null)
    })
  }

  ngOnInit() {
    this.regiaoService.listar().subscribe(resposta => {
      this.regioes = resposta;
    });
  }

  cadastrarCliente() {
    const cliente : Cliente = this.formularioCliente.value;
    cliente.endereco = this.formularioEndereco.value;

    this.clienteService.cadastrar(cliente).subscribe({
      next: clienteCriado => {
        alert("Cliente cadastrado com sucesso");
        this.loginService.salvarClienteLogado(clienteCriado)
        this.router.navigateByUrl("/home").then();
      },
      error: err => console.log(err)
    });
  }

}
