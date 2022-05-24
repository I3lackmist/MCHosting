import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomePageComponent } from '@shared/pages/home-page/home-page.component';

import { RegisterPageComponent } from '@user/pages/register-page/register-page.component';
import { LoginPageComponent } from '@user/pages/login-page/login-page.component';

import { EditGameServerComponent } from '@game-server/pages/edit-game-server/edit-game-server.component';
import { CreateGameServerComponent } from '@game-server/pages/create-game-server/create-game-server.component';
import { GameServerDetailsComponent } from '@game-server/pages/game-server-details/game-server-details.component';
import { GameServerListComponent } from '@game-server/pages/game-server-list/game-server-list.component';

const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: HomePageComponent },
    { path: 'login', component: LoginPageComponent },
    { path: 'register', component: RegisterPageComponent},
    { path: 'create', component: CreateGameServerComponent },
    { path: 'details/:gameservername', component: GameServerDetailsComponent },
    { path: 'edit/:gameservername', component: EditGameServerComponent },
    { path: 'list', component: GameServerListComponent },
    { path: '**', redirectTo: 'home' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
