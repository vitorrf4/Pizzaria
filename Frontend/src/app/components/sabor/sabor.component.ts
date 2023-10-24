import { Component } from '@angular/core';
import {Sabor} from "../../models/Sabor";
import {SaborService} from "../../services/sabor.service";

@Component({
  selector: 'app-sabor',
  templateUrl: './sabor.component.html',
  styleUrls: ['./sabor.component.css']
})
export class SaborComponent {
  sabores: Sabor[] = []

  constructor(private service: SaborService) {
    this.service.listar().subscribe(resposta => {
      this.sabores = resposta
    })
  }
}
