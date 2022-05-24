import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '@material/material.module';

import { SharedModule } from '@shared/shared.module';
import { LoginPageComponent } from '@user/pages/login-page/login-page.component';
import { RegisterPageComponent } from '@user/pages/register-page/register-page.component';
import { AccountService } from '@user/services/account-service/account.service';

@NgModule({
    declarations: [
        LoginPageComponent,
        RegisterPageComponent,
    ],
    imports: [
        ReactiveFormsModule,
        SharedModule,
        HttpClientModule,
        MaterialModule
    ],
    exports: [
        LoginPageComponent,
        RegisterPageComponent
    ],
    providers: [
        AccountService
    ]
})
export class UserModule { }
