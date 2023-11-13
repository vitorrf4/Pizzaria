import { Component } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Sabor } from 'src/app/models/Sabor';
import { SaborService } from 'src/app/services/sabor.service';

@Component({
  selector: 'app-sabor',
  templateUrl: './sabor.component.html',
  styleUrls: ['./sabor.component.css']
})
export class SaborComponent {
  sabores : Sabor[] = [];
  sabor = new Sabor();

  constructor(private saborService: SaborService) {
    saborService.listar().subscribe({
      next: resposta => {
        this.sabores = resposta;
        console.log(resposta);
      }
    })
  }

  cadastrarSabor() {
    console.log(this.sabor);

    this.saborService.cadastrar(this.sabor).subscribe({
      next: res => {
        this.sabores.push(res);
      }
    })
  }
}
