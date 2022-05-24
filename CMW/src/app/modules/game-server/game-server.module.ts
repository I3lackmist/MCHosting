import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '@material/material.module';

import { GameServerService } from '@game-server/services/game-server.service';
import { CreateGameServerComponent } from '@game-server/pages/create-game-server/create-game-server.component';
import { GameServerDetailsComponent } from '@game-server/pages/game-server-details/game-server-details.component';
import { GameServerListComponent } from '@game-server/pages/game-server-list/game-server-list.component';

import { SharedModule } from '@shared/shared.module';
import { EditGameServerComponent } from './pages/edit-game-server/edit-game-server.component';

@NgModule({
    declarations: [
        CreateGameServerComponent,
        GameServerDetailsComponent,
        GameServerListComponent,
        EditGameServerComponent
    ],
    imports: [
        SharedModule,
        ReactiveFormsModule,
        HttpClientModule,
        MaterialModule
    ],
    providers: [
        GameServerService
    ]
})
export class GameServerModule { }
