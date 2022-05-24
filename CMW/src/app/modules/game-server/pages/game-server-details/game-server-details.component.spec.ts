import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GameServerDetailsComponent } from './game-server-details.component';

describe('GameServerDetailsComponent', () => {
    let component: GameServerDetailsComponent;
    let fixture: ComponentFixture<GameServerDetailsComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            declarations: [ GameServerDetailsComponent ]
        })
        .compileComponents();
    });

    beforeEach(() => {
        fixture = TestBed.createComponent(GameServerDetailsComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
