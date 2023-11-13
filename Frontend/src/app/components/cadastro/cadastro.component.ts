import {Component, OnInit} from '@angular/core';
import {Cliente} from "../../models/Cliente";
import {Regiao} from "../../models/Regiao";
import {ClienteService} from "../../services/cliente.service";
import {FormControl, FormGroup} from "@angular/forms";
import {Router} from "@angular/router";
import {LoginService} from "../../services/login.service";
import {CepService} from "../../services/cep.service";
import { Endereco } from 'src/app/models/Endereco';

@Component({
  selector: 'app-cadastro',
  templateUrl: './cadastro.component.html',
  styleUrls: ['./cadastro.component.css']
})
export class CadastroComponent {
  formularioCliente: any;
  formularioEndereco: any;


  constructor(private clienteService: ClienteService,
              private loginService: LoginService,
              private router: Router,
              private cep: CepService) {
    this.iniciaFormularios();
  }

  iniciaFormularios() {
    this.formularioCliente = new FormGroup({
      cpf: new FormControl(null),
      nome: new FormControl(null),
      telefone: new FormControl(null),
      dataAniversario: new FormControl(null)
    });

    this.formularioEndereco = new FormGroup({
      rua: new FormControl(null),
      numero: new FormControl(null),
      cep: new FormControl(null),
      complemento: new FormControl(null),
      regiao: new FormControl(null)
    });

  }

  buscarCEP() {
    const cep = this.formularioEndereco.value.cep;

    this.cep.buscarCep(cep).subscribe({
      next : res => {
        if (!res.logradouro) {
          alert("CEP não encontrado");
          return;
        }

        this.formularioEndereco.get("rua").setValue(res.logradouro);
        this.formularioEndereco.get("regiao").setValue(res.bairro);
      }, 
      error: () => {
        alert("CEP inválido");
      }
    });

  }

  cadastrarCliente() {
    const cliente : Cliente = this.formularioCliente.value;
    const endereco = this.formularioEndereco.value;
    
    if (!this.cadastroEstaValido(cliente, endereco)) {
      return;
    }

    let nomeRegiao = this.formularioEndereco.value.regiao;
    cliente.endereco = endereco;
    cliente.endereco.regiao = new Regiao(nomeRegiao);

    this.clienteService.cadastrar(cliente).subscribe({
      next: clienteCriado => {
        alert("Cliente cadastrado com sucesso");

        this.loginService.salvarClienteLogado(clienteCriado)
        this.router.navigateByUrl("/home/clientes").then();
      },
      error: err => console.log(err)
    });
  }

  cadastroEstaValido(cliente: Cliente, endereco: Endereco): boolean {

    for (let campo of Object.entries(cliente)) {
      if (!campo[1]) {
        alert(`Preencha todos os campos`);
        return false;
      }
    }

    for (let campo of Object.entries(endereco)) {
      if (!campo[1] && campo[0] != "complemento") {
        alert("Preencha todos os campos");
        return false;
      }
    }

    return true;
  }
}
