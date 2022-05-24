import { ROOT_API_URL } from 'src/environments/environment';

import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';

import { LoginUserRequestDTO } from '@user/interfaces/DTOs/login-user-request.dto';
import { RegisterUserRequestDTO } from '@user/interfaces/DTOs/register-user-request.dto';
import { SimpleResponse } from '@app/modules/shared/interfaces/base-response.dto';

@Injectable({
    providedIn: 'root'
})
export class AccountService {
    public activeUser?: string = undefined;

    constructor(
        private _http: HttpClient
    ) {}

    public loginUser(request: LoginUserRequestDTO): Observable<HttpResponse<SimpleResponse>> {
        let subscription = this._http.post<SimpleResponse>(
            ROOT_API_URL + '/user/login',
            request,
            {
                observe: 'response'
            }
        );

        return subscription;
    }

    public registerUser(request: RegisterUserRequestDTO): Observable<HttpResponse<SimpleResponse>> {
        let subscription = this._http.post<SimpleResponse>(
            ROOT_API_URL + '/user/register',
            request,
            {
                observe: 'response'
            }
        );

        return subscription;
    }
}
