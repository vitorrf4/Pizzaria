import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TamanhoComponent } from './tamanho.component';

describe('TamanhoComponent', () => {
  let component: TamanhoComponent;
  let fixture: ComponentFixture<TamanhoComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TamanhoComponent]
    });
    fixture = TestBed.createComponent(TamanhoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
