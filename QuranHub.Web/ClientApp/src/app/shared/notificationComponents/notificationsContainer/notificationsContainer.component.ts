import { Component, HostListener } from '@angular/core';
import { NotificationRepository } from '../../../abstractions/repositories/notificationRepository';

@Component({
  selector: 'notificationsContainer',
  templateUrl: './notificationsContainer.component.html',
  styleUrls: ['./notificationsContainer.component.css']
})

export class NotificationsContainerComponent {

  showNotificationPanel: boolean = false;
  viewed:boolean = false;

  constructor(
    public notificationRepository: NotificationRepository) {}

  togglePanel(event:any) {
    event.stopPropagation();
    this.showNotificationPanel = !this.showNotificationPanel;
    this.viewed = true;
  }

  hidePanel(event:any) {
    this.showNotificationPanel = false;
  }
   
}
