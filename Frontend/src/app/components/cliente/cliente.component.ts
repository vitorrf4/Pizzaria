import { Component, OnInit } from '@angular/core';
import { ClienteService } from 'src/app/services/cliente.service';
import { FormControl, FormGroup } from '@angular/forms';


@Component({
  selector: 'app-cliente',
  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.css']
})
export class ClienteComponent implements OnInit {
  formulario: any;
  tituloFormulario: string = "Novo Cliente"

  constructor() { }

  ngOnInit(): void {
    this.formulario = new FormGroup({
      placa: new FormControl(null),
      descricao: new FormControl(null)})
  }

}
