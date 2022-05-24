import { Component } from '@angular/core';

import { AccountService } from '@user/services/account-service/account.service';

@Component({
    selector: 'app-navbar',
    templateUrl: './navbar.component.html',
    styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {

    constructor(private _accountService: AccountService ) { }

    public userLoggedIn(): boolean {
        return this._accountService.activeUser != undefined;
    }

    public getLoggedInUserName(): string {
        return this._accountService.activeUser!;
    }

}
