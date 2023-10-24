import { Component } from '@angular/core';
import {ClienteService} from "../../services/cliente.service";
import {Cliente} from "../../models/Cliente";

@Component({
  selector: 'app-cliente',
  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.css']
})
export class ClienteComponent {
  clientes: Cliente[] = []

  constructor(private service: ClienteService) {
    service.listar().subscribe(resposta => {
      this.clientes = resposta
    })
  }
}
