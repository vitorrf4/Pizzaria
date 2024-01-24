import { Endereco } from "./Endereco";

export class Cliente {
    cpf: string = "";
    nome: string = "";
    telefone: string = "";
    endereco: Endereco = new Endereco();
}
