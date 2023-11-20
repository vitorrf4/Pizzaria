import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from "@angular/common/http";
import { FormsModule, ReactiveFormsModule} from "@angular/forms";
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './components/app/app.component';
import { AreaClienteComponent } from './components/area-cliente/area-cliente.component';
import { AcompanhamentoComponent } from './components/acompanhamento/acompanhamento.component';
import { PizzasComponent } from './components/pizzas/pizzas.component';
import { CadastroComponent } from './components/cadastro/cadastro.component';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { CarrinhoComponent } from './components/carrinho/carrinho.component';
import { InicioComponent } from './components/inicio/inicio.component';

@NgModule({
  declarations: [
    AppComponent,
    AreaClienteComponent,
    AcompanhamentoComponent,
    PizzasComponent,
    CadastroComponent,
    LoginComponent,
    HomeComponent,
    CarrinhoComponent,
    InicioComponent
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
