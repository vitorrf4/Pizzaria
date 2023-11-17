import { Component } from '@angular/core';
import {Pedido} from "../../models/Pedido";
import {CarrinhoService} from "../../services/carrinho.service";
import {PedidoFinal} from "../../models/PedidoFinal";
import {LoginService} from "../../services/login.service";
import {PedidoFinalService} from "../../services/pedido-final.service";
import { PizzaPedidoService } from 'src/app/services/pizza-pedido.service';
import { forkJoin, map } from 'rxjs';

@Component({
  selector: 'app-carrinho',
  templateUrl: './carrinho.component.html',
  styleUrls: ['./carrinho.component.css']
})
export class CarrinhoComponent{
  itensCarrinho : Pedido[];
  pedidoFinal: PedidoFinal = new PedidoFinal();

  constructor(private carrinhoService: CarrinhoService,
              private clienteService: LoginService,
              private pizzaPedidoService: PizzaPedidoService,
              private pedidoFinalService: PedidoFinalService,) {
    this.itensCarrinho = this.carrinhoService.itensCarrinho;

    this.construirPedido();
  }

  construirPedido() {
    if (this.itensCarrinho.length <= 0) {
      return;
    }

    const cliente = this.clienteService.clienteLogado;
    const pizzas = this.carrinhoService.filtrarPizzasNoCarrinho();
    const acompanhamentos = this.carrinhoService.filtrarAcompanhamentosNoCarrinho();

    this.pedidoFinal = new PedidoFinal(cliente, pizzas, acompanhamentos);
  }

  removerDoCarrinho(index: number) {
    this.carrinhoService.removerDoCarrinho(index);
    this.itensCarrinho.splice(index, 1);
  }

  finalizarPedido() {
    this.construirPedido();

  // Cadastra todas as pizzas invdividualmente e só depois cadastra o pedido final
    forkJoin(this.salvarPizzas()).subscribe(pizzasComId => {
      this.pedidoFinal.pizzas = pizzasComId;
      this.salvarPedidoFinal();
    });
  }

  salvarPizzas() {
    const pizzaObservables = this.pedidoFinal.pizzas.map(pizza => {
      return this.pizzaPedidoService.cadastrar(pizza).pipe(
        map(pizzaCadastrada => { return pizzaCadastrada; })
      );
    });
  
    return pizzaObservables;
  }
  

  salvarPedidoFinal() {
    this.pedidoFinalService.cadastrar(this.pedidoFinal).subscribe({
      next: () => {
        alert("Pedido finalizado com sucesso");
        this.limparCarrinho();
      },
      error: err => console.log(err)
    });
  }


  limparCarrinho() {
    this.carrinhoService.limparCarrinho();
    this.itensCarrinho = [];
  }
}
