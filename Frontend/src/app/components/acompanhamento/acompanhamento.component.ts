import {Component, OnInit} from '@angular/core';
import {Acompanhamento} from "../../models/entities/Acompanhamento";
import {AcompanhamentoService} from "../../services/api/acompanhamento.service";
import {AcompanhamentoPedido} from "../../models/entities/AcompanhamentoPedido";
import {CarrinhoService} from "../../services/carrinho.service";

@Component({
  selector: 'app-acompanhamento',
  templateUrl: './acompanhamento.component.html',
  styleUrls: ['./acompanhamento.component.css']
})
export class AcompanhamentoComponent implements OnInit{
  acompanhamentos: Acompanhamento[] = []
  quantidadeArray: number[] = [];

  constructor(private service: AcompanhamentoService,
              private carrinhoService: CarrinhoService) { }

  ngOnInit() {
    this.service.listar().subscribe(resposta => {
      this.acompanhamentos = resposta;
    });
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
