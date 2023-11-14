import { Component, EventEmitter, Input, Output } from '@angular/core';
import { NotificationRepository } from '../../../abstractions/repositories/notificationRepository';

@Component({
  selector: "view-more-notifications",
  templateUrl: "view-more-notifications.component.html"
})

export class ViewMoreNotificationsComponent {

  constructor(public notificationRepository: NotificationRepository) {}

  onLoadMoreNotifications(event:any){ 
    event.stopPropagation();
    this.notificationRepository.loadMoreNotifications(10); 
  }
}
