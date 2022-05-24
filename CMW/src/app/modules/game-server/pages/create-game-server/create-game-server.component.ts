import { Component } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

import { CreateGameServerRequestDTO } from '@game-server/interfaces/DTOs/create-game-server-request.dto';
import { GameVersionDTO } from '@game-server/interfaces/DTOs/game-version.dto';
import { ServerFlavorDTO } from '@game-server/interfaces/DTOs/server-flavor.dto';
import { GameServerService } from '@game-server/services/game-server.service';

import { AccountService } from '@user/services/account-service/account.service';

import { NotificationService } from '@shared/services/notification.service';

@Component({
    selector: 'app-create-server',
    templateUrl: './create-game-server.component.html',
    styleUrls: ['./create-game-server.component.scss']
})
export class CreateGameServerComponent {
    public availableServerFlavors?: ServerFlavorDTO[];
    public availableGameVersions?: GameVersionDTO[];

    public acceptingInput: boolean = true;

    public createGameServerForm: FormGroup = this._formBuilder.group({
        gameServerName: '',
        serverFlavorName: '',
        gameVersionName: ''
    });

    constructor(
        private _formBuilder: FormBuilder,
        private _router: Router,
        private _accountService: AccountService,
        private _gameServerService: GameServerService,
        private _notificationService: NotificationService
    ) {
        this._gameServerService.getAvailableFlavors().subscribe( (response) => {
            this.availableServerFlavors = response;
        });

        this._gameServerService.getAvailableVersions().subscribe( (response) => {
            this.availableGameVersions = response;
        });
    }

    public submitCreateGameServerForm(): void {
        if (this._accountService.activeUser == null || this._accountService.activeUser == "") {
            return;
        }

        this.acceptingInput = false;

        let request: CreateGameServerRequestDTO = {} as CreateGameServerRequestDTO;

        request.requestingUserName = this._accountService.activeUser;
        request.gameServerName = this.createGameServerForm.get('gameServerName')?.value;
        request.serverFlavorName = this.createGameServerForm.get('serverFlavorName')?.value;
        request.gameVersionName = this.createGameServerForm.get('gameVersionName')?.value;

        this._gameServerService.requestNewGameServer(request)
            .subscribe( response  => {
                if (!response.ok) {
                    this.acceptingInput = true;
                    this._notificationService.displayErrorMessage(response.body?.message ?? "");
                    return;
                }

                this._router.navigate(['home']);
                this._notificationService.displaySuccessMessage(response.body?.message ?? "");
            }
        );

        this._notificationService.displayInfoMessage("Requested server");
    }
}
