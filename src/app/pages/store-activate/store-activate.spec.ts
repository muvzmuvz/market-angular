import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StoreActivate } from './store-activate';

describe('StoreActivate', () => {
  let component: StoreActivate;
  let fixture: ComponentFixture<StoreActivate>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StoreActivate]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StoreActivate);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
