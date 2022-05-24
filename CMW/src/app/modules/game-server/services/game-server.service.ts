import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';

import { ROOT_API_URL } from 'src/environments/environment';

import { CreateGameServerRequestDTO } from '@game-server/interfaces/DTOs/create-game-server-request.dto';
import { EditGameServerRequestDTO } from '@game-server/interfaces/DTOs/edit-game-server-request.dto';
import { GameServerDTO } from '@game-server/interfaces/DTOs/game-server.dto';
import { ServerFlavorDTO } from '@game-server/interfaces/DTOs/server-flavor.dto';
import { GameVersionDTO } from '@game-server/interfaces/DTOs/game-version.dto';

import { AccountService } from '@user/services/account-service/account.service';

import { SimpleResponse } from '@app/modules/shared/interfaces/base-response.dto';

@Injectable({
    providedIn: 'any'
})
export class GameServerService {

    constructor(
        private _httpClient: HttpClient,
        private _accountService: AccountService
    ) { }

    public getAvailableFlavors(): Observable<ServerFlavorDTO[]> {
        return this._httpClient.get<ServerFlavorDTO[]>(
            ROOT_API_URL + '/serverflavor/all'
        );
    }

    public getAvailableVersions(): Observable<GameVersionDTO[]> {
        return this._httpClient.get<GameVersionDTO[]>(
            ROOT_API_URL + '/gameversion',
        );
    }

    public requestNewGameServer(request: CreateGameServerRequestDTO): Observable<HttpResponse<SimpleResponse>> {
        let subscription = this._httpClient.post<SimpleResponse>(
            ROOT_API_URL + '/gameserver/create',
            request,
            {
                headers: {
                    'Content-Type': 'application/json'
                },
                observe: 'response'
            }
        );

        return subscription;
    }

    public getUsersGameServers(userName: string): Observable<GameServerDTO[]> {
        return this._httpClient.get<GameServerDTO[]>(
            ROOT_API_URL + '/gameserver/list/',
            {
                params: {
                    username: userName
                }
            }
        );
    }

    public getGameServer(gameServerName: string): Observable<GameServerDTO> {
        return this._httpClient.get<GameServerDTO>(
            ROOT_API_URL + '/gameserver',
            {
                params: {
                    gameservername: gameServerName
                }
            }
        );
    }

    public editGameServer(request: EditGameServerRequestDTO): Observable<string> {
        return this._httpClient.post<string>(ROOT_API_URL + '/gameserver/edit', request);
    }

    public deleteGameServer(gameServerName: string): Observable<HttpResponse<SimpleResponse>> {
        let subscription = this._httpClient.delete<SimpleResponse>(
            ROOT_API_URL + '/gameserver/delete',
            {
                params: {
                    gameservername: gameServerName
                },
                headers: {
                    requestingUserName: this._accountService.activeUser ?? ''
                },
                observe: 'response'
            }
        );

        return subscription;
    }
}
