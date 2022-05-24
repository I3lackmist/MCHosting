import { Component } from '@angular/core';

import { GameServerDTO } from '@game-server/interfaces/DTOs/game-server.dto';
import { GameServerService } from '@game-server/services/game-server.service';
import { AccountService } from '@user/services/account-service/account.service';

@Component({
    selector: 'app-server-list',
    templateUrl: './game-server-list.component.html',
    styleUrls: ['./game-server-list.component.scss']
})
export class GameServerListComponent {

    public gameServers?: GameServerDTO[];

    constructor(
        private _accountService: AccountService,
        private _gameServerService: GameServerService
    ) {
        this._gameServerService
            .getUsersGameServers(this._accountService.activeUser!)
            .subscribe( response => {
                this.gameServers = response;
            }
        );
    }

}
