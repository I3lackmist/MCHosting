import { HttpResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { GameServerDTO } from '@game-server/interfaces/DTOs/game-server.dto';
import { GameServerService } from '@game-server/services/game-server.service';
import { SimpleResponse } from '@shared/interfaces/base-response.dto';
import { NotificationService } from '@shared/services/notification.service';

@Component({
    selector: 'app-game-server-details',
    templateUrl: './game-server-details.component.html',
    styleUrls: ['./game-server-details.component.scss']
})
export class GameServerDetailsComponent {
    private _serverName: string;
    public gameServer?: GameServerDTO;

    constructor(
        private _activatedRoute: ActivatedRoute,
        private _router: Router,
        private _gameServerService: GameServerService,
        private _notificationService: NotificationService
    ) {
        this._serverName = this._activatedRoute.snapshot.paramMap.get('gameservername') ?? '';
        
        this._gameServerService.getGameServer(this._serverName).subscribe( response => {
            this.gameServer = response
        })
    }

    public getServerIp(): string {
        if (this.gameServer?.ip == '') {
            return 'No IP address assigned yet';
        }
        else return this.gameServer?.ip ?? '';
    }

    public deleteServer(): void {
        this._gameServerService.deleteGameServer(this._serverName).subscribe( (response: HttpResponse<SimpleResponse>) => {
            
            if (!response.ok) {
                this._notificationService.displayErrorMessage(response.body?.message ?? "Delete failed.");
            }

            this._router.navigate(['home']);
            this._notificationService.displaySuccessMessage(response.body?.message ?? "Success.");
        })
    }
}
