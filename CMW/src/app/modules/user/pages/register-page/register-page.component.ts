import { LoginUserRequestDTO } from './../../interfaces/DTOs/login-user-request.dto';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpResponse } from '@angular/common/http';

import { RegisterUserRequestDTO } from '@app/modules/user/interfaces/DTOs/register-user-request.dto';
import { AccountService } from '@user/services/account-service/account.service';
import { NotificationService } from '@shared/services/notification.service';
import { SimpleResponse } from '@shared/interfaces/base-response.dto';

@Component({
    selector: 'app-register-page',
    templateUrl: './register-page.component.html',
    styleUrls: ['./register-page.component.scss']
})


export class RegisterPageComponent {
    public registerForm: FormGroup = this._formBuilder.group({
        userName: '',
        password: '',
        passwordrepeat: '',
        email: ''
    });
    hide = true;
    constructor(
        private _formBuilder: FormBuilder,
        private _router: Router,
        private _accountService: AccountService,
        private _notificationService: NotificationService
    ) {}
    public submitRegisterForm(): void {
        if (this.registerForm.get('password')?.value !== this.registerForm.get('passwordrepeat')?.value) {
            this._notificationService.displayErrorMessage("Passwords do not match.")
            return;
        }

        let registerUserDTO: RegisterUserRequestDTO = {} as RegisterUserRequestDTO;

        registerUserDTO.userName = this.registerForm.get('userName')?.value;
        registerUserDTO.email = this.registerForm.get('email')?.value;
        registerUserDTO.password = this.registerForm.get('password')?.value;

        this._accountService.registerUser(registerUserDTO).subscribe( (response: HttpResponse<SimpleResponse>) => {
            if (!response.ok) {
                this._notificationService.displayErrorMessage(response.body?.message ?? "Registration failed.");
                return;
            }

            this._notificationService.displaySuccessMessage(response.body?.message ?? "Registered.");

            let loginUserDTO: LoginUserRequestDTO = {} as LoginUserRequestDTO;
            loginUserDTO.userName = registerUserDTO.userName;
            loginUserDTO.password = registerUserDTO.password;

            this._accountService.loginUser(loginUserDTO).subscribe( (response) => {
                if (response.ok) {
                    this._accountService.activeUser = loginUserDTO.userName;
                    this._router.navigate(['home']);
                }
            });
        });
    }
}
