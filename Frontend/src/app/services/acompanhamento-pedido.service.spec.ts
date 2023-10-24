import { TestBed } from '@angular/core/testing';

import { AcompanhamentoPedidoService } from './acompanhamento-pedido.service';

describe('AcompanhamentoPedidoService', () => {
  let service: AcompanhamentoPedidoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AcompanhamentoPedidoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
