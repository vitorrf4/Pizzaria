import {Component, OnInit} from '@angular/core';
import {ClienteService} from "../../services/cliente.service";
import {Cliente} from "../../models/Cliente";
import {FormControl, FormGroup} from '@angular/forms';
import {RegiaoService} from 'src/app/services/regiao.service';
import {Regiao} from 'src/app/models/Regiao';
import {EnderecoService} from "../../services/endereco.service";

@Component({
  selector: 'app-cliente',
  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.css']
})
export class ClienteComponent implements OnInit {
  formularioCliente: any;
  formularioEndereco: any;
  clientes: Cliente[] = [];
  regioes: Regiao[] = [];

  constructor(private clienteService: ClienteService, private regiaoService: RegiaoService,
              private enderecoService: EnderecoService) {
    this.clienteService.listar().subscribe(resposta => {
      this.clientes = resposta;
    });

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
    let endereco = this.formularioEndereco.value;

    cliente.endereco = endereco;

    console.log("cliente: ");
    console.log(cliente);

    this.clienteService.cadastrar(cliente).subscribe({
      next: () => {
        alert("Cliente cadastrado com sucesso")
      },
      error: err => {
        console.log(err);
      }
    });
  }
}
