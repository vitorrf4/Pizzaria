import {PizzaPedidoDto} from "./PizzaPedidoDto";
import {PedidoFinal} from "./PedidoFinal";

export class PedidoDto {
  clienteId: number = 0;
  pizzas : PizzaPedidoDto[] = []
  acompanhamentos : Map<string, number> = new Map<string, number>();

  constructor(pedidoFinal: PedidoFinal) {
    pedidoFinal.pizzas.forEach(p => {
      const sabores = p.sabores.map(s =>  s.nome);
      let pizzaPedidoDto = new PizzaPedidoDto(sabores, p.tamanho.nome, p.quantidade);
      this.pizzas.push(pizzaPedidoDto);
    });
    pedidoFinal.acompanhamentos.forEach(a => {
      this.acompanhamentos.set(a.acompanhamento.nome, a.quantidade);
    });
    this.clienteId = pedidoFinal.clienteId;
  }
}
