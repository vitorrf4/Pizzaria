import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-inicio',
  templateUrl: './inicio.component.html',
  styleUrls: ['./inicio.component.css']
})
export class InicioComponent implements OnInit {
  constructor(private loginService: LoginService,
              private router : Router) { }
  
  async ngOnInit() {
    if (this.loginService.estaLogado) {
      await this.router.navigateByUrl("/home/area-cliente");
    }
  }
}
