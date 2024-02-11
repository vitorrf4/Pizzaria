import {Sabor} from "./entities/Sabor";
import {Tamanho} from "./entities/Tamanho";

export function CalcularPrecoPizza(sabores: Sabor[], tamanho: Tamanho, quantidade: number): number {
  if (quantidade <= 0)
    return 0;

  let total = 0;

  sabores.forEach(sabor => total += sabor.preco);
  total *= tamanho.multiplicadorPreco;

  if (sabores.length > 1) {
    total /= sabores.length;
  }

  return total *= quantidade;
}
