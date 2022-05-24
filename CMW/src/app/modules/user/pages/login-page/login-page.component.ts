import { Component, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';

import { LoginUserRequestDTO } from '@user/interfaces/DTOs/login-user-request.dto';
import { AccountService } from '@user/services/account-service/account.service';

import { NotificationComponent } from '@shared/components/notification/notification.component';
import { NotificationService } from '@shared/services/notification.service';
import { SimpleResponse } from '@app/modules/shared/interfaces/base-response.dto';
import { HttpResponse } from '@angular/common/http';

@Component({
    selector: 'app-login-page',
    templateUrl: './login-page.component.html',
    styleUrls: ['./login-page.component.scss']
})

export class LoginPageComponent {
    public loginForm: FormGroup = this._formBuilder.group({
        userName: '',
        password: ''
    });

    constructor(
        private _formBuilder: FormBuilder,
        private _router: Router,
        private _accountService: AccountService,
        private _notificationService: NotificationService
    ) { }

    hide = true;

    public submitLoginForm(): void {
        let loginUserDTO: LoginUserRequestDTO = {} as LoginUserRequestDTO;

        loginUserDTO.userName = this.loginForm.get('userName')?.value;
        loginUserDTO.password = this.loginForm.get('password')?.value;

        this._accountService.loginUser(loginUserDTO).subscribe( (response: HttpResponse<SimpleResponse>) => {
            if (!response.ok) {
                this._notificationService.displayErrorMessage(response.body?.message ?? "Login failed.");
                return;
            }

            this._accountService.activeUser = loginUserDTO.userName;
            this._notificationService.displaySuccessMessage(response.body?.message ?? "Logged in.");

            this._router.navigate(['home']);
        });
    }
}
