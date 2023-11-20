import {Component} from '@angular/core';
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
  // Array que guarda a quantidade colocada no input de cada acompanhamento
  quantidadeArray: number[] = [];

  constructor(private service: AcompanhamentoService, private carrinhoService: CarrinhoService) {
    this.service.listar().subscribe(resposta => {
      this.acompanhamentos = resposta;
    })
  }

  adicionarAoCarrinho(acompanhamento : Acompanhamento, quantidade: number) {
    if (!quantidade || quantidade <= 0) {
      alert("Quantidade inválida");
      return;
    }

    const acompanhamentoPedido = new AcompanhamentoPedido(acompanhamento, quantidade);

    this.carrinhoService.adicionarNoCarrinho(acompanhamentoPedido)
  }
}
