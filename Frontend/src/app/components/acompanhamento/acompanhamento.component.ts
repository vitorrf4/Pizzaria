import { Component } from '@angular/core';
import {Acompanhamento} from "../../models/Acompanhamento";
import {AcompanhamentoService} from "../../services/acompanhamento.service";

@Component({
  selector: 'app-acompanhamento',
  templateUrl: './acompanhamento.component.html',
  styleUrls: ['./acompanhamento.component.css']
})
export class AcompanhamentoComponent {
  acompanhamentos: Acompanhamento[] = []

  constructor(private service: AcompanhamentoService) {
    this.service.listar().subscribe(resposta => {
      this.acompanhamentos = resposta
    })
  }
}
