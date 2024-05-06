import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AbrigoAjudaSobreComponent } from './abrigo-ajuda-sobre.component';

describe('AbrigoAjudaSobreComponent', () => {
  let component: AbrigoAjudaSobreComponent;
  let fixture: ComponentFixture<AbrigoAjudaSobreComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AbrigoAjudaSobreComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AbrigoAjudaSobreComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
