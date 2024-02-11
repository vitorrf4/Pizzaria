export class PizzaPedidoDto {
  sabores : string[];
  tamanho: string;
  quantidade: number;

  constructor(sabores: string[], tamanho: string, quantidade: number = 1) {
    this.sabores = sabores;
    this.tamanho = tamanho;
    this.quantidade = quantidade;
  }
}
