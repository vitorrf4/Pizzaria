import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AcompanhamentoPedidoComponent } from './acompanhamento-pedido.component';

describe('AcompanhamentoPedidoComponent', () => {
  let component: AcompanhamentoPedidoComponent;
  let fixture: ComponentFixture<AcompanhamentoPedidoComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AcompanhamentoPedidoComponent]
    });
    fixture = TestBed.createComponent(AcompanhamentoPedidoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
