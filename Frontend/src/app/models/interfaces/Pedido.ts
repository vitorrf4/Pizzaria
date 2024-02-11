export abstract class Pedido {
  quantidade: number = 0;
  preco: number = 0;

  getDescricao() : string {
    return `Quantidade: ${this.quantidade} | Preço: ${this.preco}`;
  };
}
