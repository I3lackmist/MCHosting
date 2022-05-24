import { Component } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

import { GameVersionDTO } from '@game-server/interfaces/DTOs/game-version.dto';
import { ServerFlavorDTO } from '@game-server/interfaces/DTOs/server-flavor.dto';
import { EditGameServerRequestDTO } from '@game-server/interfaces/DTOs/edit-game-server-request.dto';
import { GameServerService } from '@game-server/services/game-server.service';

import { NotificationService } from '@shared/services/notification.service';

import { AccountService } from '@user/services/account-service/account.service';

@Component({
    selector: 'app-edit-game-server',
    templateUrl: './edit-game-server.component.html',
    styleUrls: ['./edit-game-server.component.scss']
})
export class EditGameServerComponent {

    public availableServerFlavors?: ServerFlavorDTO[];
    public availableGameVersions?: GameVersionDTO[];

    public editGameServerForm: FormGroup = this._formBuilder.group({
        nameChange: '',
        flavorChange: '',
        versionChange: ''
    });

    constructor(
        private _formBuilder: FormBuilder,
        private _router: Router,
        private _activatedRoute: ActivatedRoute,
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

        // public submitEditGameServerForm(): void {
        //     let request: EditGameServerRequestDTO = {} as EditGameServerRequestDTO;
        
        //     request.requestingUserName = this._accountService.activeUser!;
        //     request.serverName = this._activatedRoute.snapshot.paramMap.get('gameservername') ?? "";
        //     request.versionChange = this.editGameServerForm.get('versionChange')?.value;
        //     request.flavorChange = this.editGameServerForm.get('flavorChange')?.value;
        //     request.nameChange = this.editGameServerForm.get('nameChange')?.value;

        //     this._gameServerService.editGameServer(request).subscribe( response => {
        //         if(response.success) {
        //             this._notificationService.displaySuccessMessage(response.message);
        //             this._router.navigate(['list']);
        //         }
        //         else {
        //             this._notificationService.displayErrorMessage(response.message);
        //         }
        //     });
        // }
    }
