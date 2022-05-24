import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-home-page',
    templateUrl: './home-page.component.html',
    styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent implements OnInit{
    autoTicks = false;
    disabled = false;
    invert = false;
    max = 100;
    min = 0;
    showTicks = false;
    step = 1;
    thumbLabel = false;
    value = 0;
    vertical = false;
    tickInterval = 1;

    constructor() { }

    ngOnInit() {}

    getSliderTickInterval(): number | 'auto' {
        if (this.showTicks) {
            return this.autoTicks ? 'auto' : this.tickInterval;
        }
        return 0;
    }
}