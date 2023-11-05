import { Component } from '@angular/core';
import {Sabor} from "../../models/Sabor";
import {SaborService} from "../../services/sabor.service";

@Component({
  selector: 'app-sabor',
  templateUrl: './pizzas.component.html',
  styleUrls: ['./pizzas.component.css']
})
export class PizzasComponent {
  sabores: Sabor[] = []

  constructor(private service: SaborService) {
    this.service.listar().subscribe(resposta => {
      this.sabores = resposta;
    })
  }
}
