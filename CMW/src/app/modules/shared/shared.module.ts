import { NotificationService } from './services/notification.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MaterialModule } from '@material/material.module';

import { NavbarComponent } from '@shared/components/navbar/navbar.component';
import { NotificationComponent } from '@shared/components/notification/notification.component';
import { HomePageComponent } from '@shared/pages/home-page/home-page.component';

@NgModule({
    imports: [
        CommonModule,
        RouterModule,
        MaterialModule
    ],
    declarations: [
        NavbarComponent,
        HomePageComponent,
        NotificationComponent,
    ],
    exports: [
        CommonModule,
        RouterModule,
        NavbarComponent,
        HomePageComponent,
        NotificationComponent,
    ],
    providers: [
        NotificationService
    ]
})
export class SharedModule { }
