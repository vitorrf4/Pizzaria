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
export class ClienteComponent {
  clientes: Cliente[] = [];

  constructor(private clienteService: ClienteService) {
    this.clienteService.listar().subscribe(resposta => {
      this.clientes = resposta;
    });
  }
}
