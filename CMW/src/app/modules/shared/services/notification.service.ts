import { Injectable } from '@angular/core';

import { NotificationComponent } from '@shared/components/notification/notification.component';

@Injectable({
    providedIn: 'root'
})
export class NotificationService {

    private _notificationComponent?: NotificationComponent;

    constructor() { }

    public registerNotificationCompoenent(notificationComponent: NotificationComponent) {
        this._notificationComponent = notificationComponent;
    }

    public displayErrorMessage(errorMessage: string): void {
        this._notificationComponent?.displayErrorMessage(errorMessage.toString());
    }

    public displaySuccessMessage(successMessage: string): void {
        this._notificationComponent?.displaySuccessMessage(successMessage.toString());
    }

    public displayInfoMessage(infoMessage: string): void {
        this._notificationComponent?.displayInfoMessage(infoMessage.toString());
    }
}
