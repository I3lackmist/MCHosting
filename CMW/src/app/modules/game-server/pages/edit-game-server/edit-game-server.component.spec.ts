import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditGameServerComponent } from './edit-game-server.component';

describe('EditGameServerComponent', () => {
  let component: EditGameServerComponent;
  let fixture: ComponentFixture<EditGameServerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditGameServerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditGameServerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
