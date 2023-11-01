import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from "@angular/common/http";
import { FormsModule, ReactiveFormsModule} from "@angular/forms";
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './components/app/app.component';
import { ClienteComponent } from './components/cliente/cliente.component';
import { EnderecoComponent } from './components/endereco/endereco.component';
import { RegiaoComponent } from './components/regiao/regiao.component';
import { AcompanhamentoComponent } from './components/acompanhamento/acompanhamento.component';
import { AcompanhamentoPedidoComponent } from './components/acompanhamento-pedido/acompanhamento-pedido.component';
import { PedidoFinalComponent } from './components/pedido-final/pedido-final.component';
import { PizzaPedidoComponent } from './components/pizza-pedido/pizza-pedido.component';
import { PromocaoComponent } from './components/promocao/promocao.component';
import { SaborComponent } from './components/sabor/sabor.component';
import { TamanhoComponent } from './components/tamanho/tamanho.component';
import { CadastroComponent } from './components/cadastro/cadastro.component';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';

@NgModule({
  declarations: [
    AppComponent,
    ClienteComponent,
    EnderecoComponent,
    RegiaoComponent,
    AcompanhamentoComponent,
    AcompanhamentoPedidoComponent,
    PedidoFinalComponent,
    PizzaPedidoComponent,
    PromocaoComponent,
    SaborComponent,
    TamanhoComponent,
    CadastroComponent,
    LoginComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
