import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {ClienteComponent} from "./components/cliente/cliente.component";
import {AcompanhamentoComponent} from "./components/acompanhamento/acompanhamento.component";
import {SaborComponent} from "./components/sabor/sabor.component";
import {TamanhoComponent} from "./components/tamanho/tamanho.component";

const routes: Routes = [
  {path: "clientes", component: ClienteComponent},
  {path: "acompanhamentos", component: AcompanhamentoComponent},
  {path: "sabores", component: SaborComponent},
  {path: "tamanhos", component: TamanhoComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
