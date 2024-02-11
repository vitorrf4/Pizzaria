import { Endereco } from "./Endereco";
import { Usuario } from "./Usuario";

export class Cliente extends Usuario {
    nome: string = "";
    telefone: string = "";
    endereco: Endereco = new Endereco();
}
