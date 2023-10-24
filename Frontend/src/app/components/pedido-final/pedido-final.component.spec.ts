import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PedidoFinalComponent } from './pedido-final.component';

describe('PedidoFinalComponent', () => {
  let component: PedidoFinalComponent;
  let fixture: ComponentFixture<PedidoFinalComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PedidoFinalComponent]
    });
    fixture = TestBed.createComponent(PedidoFinalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
