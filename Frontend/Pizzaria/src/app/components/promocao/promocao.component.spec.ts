import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PromocaoComponent } from './promocao.component';

describe('PromocaoComponent', () => {
  let component: PromocaoComponent;
  let fixture: ComponentFixture<PromocaoComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PromocaoComponent]
    });
    fixture = TestBed.createComponent(PromocaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
