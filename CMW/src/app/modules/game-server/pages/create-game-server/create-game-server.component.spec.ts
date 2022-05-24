import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateGameServerComponent as CreateGameServerComponent } from './create-game-server.component';

describe('CreateGameServerComponent', () => {
    let component: CreateGameServerComponent;
    let fixture: ComponentFixture<CreateGameServerComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [ CreateGameServerComponent ]
        })
        .compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(CreateGameServerComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
