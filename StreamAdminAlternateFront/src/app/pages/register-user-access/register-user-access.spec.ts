import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RegisterUserAccess } from './register-user-access';

describe('RegisterUserAccess', () => {
  let component: RegisterUserAccess;
  let fixture: ComponentFixture<RegisterUserAccess>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegisterUserAccess],
    }).compileComponents();

    fixture = TestBed.createComponent(RegisterUserAccess);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
