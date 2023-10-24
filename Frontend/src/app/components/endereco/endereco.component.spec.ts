import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnderecoComponent } from './endereco.component';

describe('EnderecoComponent', () => {
  let component: EnderecoComponent;
  let fixture: ComponentFixture<EnderecoComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EnderecoComponent]
    });
    fixture = TestBed.createComponent(EnderecoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
