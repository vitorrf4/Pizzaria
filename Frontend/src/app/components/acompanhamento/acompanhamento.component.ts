import {Component, ElementRef, ViewChild} from '@angular/core';
import {Acompanhamento} from "../../models/Acompanhamento";
import {AcompanhamentoService} from "../../services/acompanhamento.service";
import {AcompanhamentoPedido} from "../../models/AcompanhamentoPedido";
import {CarrinhoService} from "../../services/carrinho.service";

@Component({
  selector: 'app-acompanhamento',
  templateUrl: './acompanhamento.component.html',
  styleUrls: ['./acompanhamento.component.css']
})
export class AcompanhamentoComponent {
  acompanhamentos: Acompanhamento[] = []
  // ViewChild é o equivalente do getElementById em JavaScript, só que selecionara o elemento
  // definido com uma "#"
  @ViewChild("quantidade") quantidadeInput! : ElementRef;

  constructor(private service: AcompanhamentoService, private carrinhoService: CarrinhoService) {
    this.service.listar().subscribe(resposta => {
      this.acompanhamentos = resposta;
    })
  }

  adicionarAoCarrinho(acompanhamento : Acompanhamento) {
    const quantidade = this.quantidadeInput.nativeElement.value;
    const acompanhamentoPedido = new AcompanhamentoPedido(acompanhamento, quantidade);

    this.carrinhoService.adicionarNoCarrinho(acompanhamentoPedido)
  }
}
