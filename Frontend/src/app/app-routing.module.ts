import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AreaClienteComponent } from "./components/area-cliente/area-cliente.component";
import { AcompanhamentoComponent } from "./components/acompanhamento/acompanhamento.component";
import { PizzasComponent } from "./components/pizzas/pizzas.component";
import { CadastroComponent } from "./components/cadastro/cadastro.component";
import { LoginComponent } from "./components/login/login.component";
import { HomeComponent } from "./components/home/home.component";
import { CarrinhoComponent } from "./components/carrinho/carrinho.component";
import {InicioComponent} from "./components/inicio/inicio.component";
import {PedidosComponent} from "./components/pedidos/pedidos.component";

const routes: Routes = [
  { path: "", component: InicioComponent,
    children : [
      { path: "login", component: LoginComponent },
      { path: "cadastro", component: CadastroComponent },
    ]},
  { path: "home", component: HomeComponent,
    children: [
      { path: "area-cliente", component: AreaClienteComponent },
      { path: "pedidos", component: PedidosComponent },
      { path: "acompanhamentos", component: AcompanhamentoComponent },
      { path: "sabores", component: PizzasComponent },
      { path: "carrinho", component: CarrinhoComponent },
    ]},
  { path: "**", redirectTo: "/login" } // Rota padrão para tratamento de rotas não reconhecidas
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
