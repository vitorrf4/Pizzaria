import {PizzaPedidoDto} from "./PizzaPedidoDto";
import {PedidoFinal} from "../entities/PedidoFinal";
import {AcompanhamentoPedidoDto} from "./AcompanhamentoPedidoDto";

export class PedidoDto {
  clienteId: number = 0;
  pizzas : PizzaPedidoDto[] = []
  acompanhamentos : AcompanhamentoPedidoDto[] = [];

  constructor(pedidoFinal: PedidoFinal) {
    pedidoFinal.pizzas.forEach(p => {
      const sabores = p.sabores.map(s =>  s.nome);
      let pizzaPedidoDto = new PizzaPedidoDto(sabores, p.tamanho.nome, p.quantidade);
      this.pizzas.push(pizzaPedidoDto);
    });
    pedidoFinal.acompanhamentos.forEach(a => {
      this.acompanhamentos.push(new AcompanhamentoPedidoDto(a.acompanhamento.nome, a.quantidade));
    });
    this.clienteId = pedidoFinal.cliente.id;
  }
}
