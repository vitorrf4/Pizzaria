export class AcompanhamentoPedidoDto {
  acompanhamento: string = "";
  quantidade: number = 0;

  constructor(acompanhamento: string, quantidade: number) {
    this.acompanhamento = acompanhamento;
    this.quantidade = quantidade;
  }
}
