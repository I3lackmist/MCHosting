import { Component } from '@angular/core';
import { NotificationService } from '@shared/services/notification.service';

@Component({
    selector: 'app-notification',
    templateUrl: './notification.component.html',
    styleUrls: ['./notification.component.scss']
})
export class NotificationComponent {
    public notificationMessage: string = "";
    public notificationStyle: string = "";
    public showNotification: boolean = false;

    constructor(private _notificationService: NotificationService) {
        this._notificationService.registerNotificationCompoenent(this);
    }

    public displayErrorMessage(errorMessage: string): void {
        this.notificationMessage = errorMessage;
        this.notificationStyle = "error";
        this.showNotification = true;
        this.hideMessage();
    }

    public displaySuccessMessage(successMessage: string): void {
        this.notificationMessage = successMessage;
        this.notificationStyle = "success";
        this.showNotification = true;
        this.hideMessage();
    }

    public displayInfoMessage(infoMessage: string): void {
        this.notificationMessage = infoMessage;
        this.notificationStyle = "info";
        this.showNotification = true;
        this.hideMessage();
    }

    private hideMessage(): void {
        setTimeout( () => {
            this.notificationMessage = "";
            this.notificationStyle = "";
            this.showNotification = false;
        }, 3000);
    }
}
