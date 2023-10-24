import { Component } from '@angular/core';
import {ClienteService} from "../../services/cliente.service";

@Component({
  selector: 'app-cliente',
  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.css']
})
export class ClienteComponent {
  constructor(private service: ClienteService) {
  }
}
