import { Component } from '@angular/core';
import {ClienteService} from "../../services/cliente.service";
import {Cliente} from "../../models/Cliente";
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-cliente',
  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.css']
})
export class ClienteComponent {
  formulario: any;
  clientes: Cliente[] = [];

  constructor(private service: ClienteService) {
    this.service.listar().subscribe(resposta => {
      this.clientes = resposta
    });
    this.formulario = new FormGroup({
      cpf: new FormControl(null),
      nome: new FormControl(null),
      telefone: new FormControl(null),
      dataAniversario: new FormControl(null)
    })
  }

  cadastrarCliente() {
    const cliente = this.formulario.value;

    this.service.cadastrar(cliente).subscribe({
      next: () => { alert("Cliente cadastrado com sucesso")},
      error: err => {console.log("erro: " + err.message)}
    });
  }
}
