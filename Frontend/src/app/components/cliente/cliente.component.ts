import { Component } from '@angular/core';
import {ClienteService} from "../../services/cliente.service";
import {Cliente} from "../../models/Cliente";
import { FormControl, FormGroup } from '@angular/forms';
import { RegiaoService } from 'src/app/services/regiao.service';
import { Regiao } from 'src/app/models/Regiao';

@Component({
  selector: 'app-cliente',
  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.css']
})
export class ClienteComponent {
  formulario: any;
  clientes: Cliente[] = [];
  regioes: Regiao[] = [];

  constructor(private clienteService: ClienteService, private regiaoService: RegiaoService) {

    this.clienteService.listar().subscribe(resposta => {
      this.clientes = resposta
    });

    this.formulario = new FormGroup({
      cpf: new FormControl(null),
      nome: new FormControl(null),
      telefone: new FormControl(null),
      dataAniversario: new FormControl(null)
    })
  }

  ngOnInit() {
    this.regiaoService.listar().subscribe(resposta => {
      this.regioes = resposta;
    });
    
    console.log(this.regioes);

  }

  cadastrarCliente() {
    const cliente = this.formulario.value;

    this.clienteService.cadastrar(cliente).subscribe({
      next: () =>  alert("Cliente cadastrado com sucesso"),
      error: err =>  console.log("erro: " + err.message) 
    });
  }
}
