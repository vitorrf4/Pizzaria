import { Cliente } from "./Cliente";
import { Regiao } from "./Regiao";

export class Endereco {
    id: number = 0;
    rua: string = "";
    numero: number = 0;
    cep: string = "";
    complemento: string = "";
    cidade: string = "";
    estado: string = "";
    regiao: Regiao = new Regiao();
    cliente: Cliente = new Cliente();
}
