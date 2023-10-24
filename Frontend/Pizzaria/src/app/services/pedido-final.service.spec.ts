import { TestBed } from '@angular/core/testing';

import { PedidoFinalService } from './pedido-final.service';

describe('PedidoFinalService', () => {
  let service: PedidoFinalService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PedidoFinalService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
