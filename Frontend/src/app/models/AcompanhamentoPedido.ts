import { Acompanhamento } from "./Acompanhamento";
import { PedidoFinal } from "./PedidoFinal";

export class AcompanhamentoPedido {
    id: number = 0;
    acompanhamento: Acompanhamento = new Acompanhamento();
    quantidade: number = 0;
    precoTotal: number = 0;
    pedidoFinal: PedidoFinal = new PedidoFinal();
}
