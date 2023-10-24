import { Component } from '@angular/core';
import {TamanhoService} from "../../services/tamanho.service";
import {Tamanho} from "../../models/Tamanho";

@Component({
  selector: 'app-tamanho',
  templateUrl: './tamanho.component.html',
  styleUrls: ['./tamanho.component.css']
})
export class TamanhoComponent {
  tamanhos: Tamanho[] = []

  constructor(private service: TamanhoService) {
    this.service.listar().subscribe(resposta => {
      this.tamanhos = resposta
    })
  }
}
