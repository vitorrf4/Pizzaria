import { TestBed } from '@angular/core/testing';

import { PizzaPedidoService } from './pizza-pedido.service';

describe('PizzaPedidoService', () => {
  let service: PizzaPedidoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PizzaPedidoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
