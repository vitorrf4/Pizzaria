import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {ClienteComponent} from "./components/cliente/cliente.component";
import {AcompanhamentoComponent} from "./components/acompanhamento/acompanhamento.component";
import {SaborComponent} from "./components/sabor/sabor.component";
import {TamanhoComponent} from "./components/tamanho/tamanho.component";
import {CadastroComponent} from "./components/cadastro/cadastro.component";
import {LoginComponent} from "./components/login/login.component";
import {HomeComponent} from "./components/home/home.component";

const routes: Routes = [
  {path: "login", component: LoginComponent},
  {path: "cadastro", component: CadastroComponent},
  {
    path: "home", component: HomeComponent,
    children: [
      {path: "clientes", component: ClienteComponent},
      {path: "acompanhamentos", component: AcompanhamentoComponent},
      {path: "sabores", component: SaborComponent},
      {path: "tamanhos", component: TamanhoComponent},
    ]},
  {path: "**", component: LoginComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
