import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExpensedetailComponent } from './expensedetail.component';

describe('ExpensedetailComponent', () => {
  let component: ExpensedetailComponent;
  let fixture: ComponentFixture<ExpensedetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExpensedetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExpensedetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
