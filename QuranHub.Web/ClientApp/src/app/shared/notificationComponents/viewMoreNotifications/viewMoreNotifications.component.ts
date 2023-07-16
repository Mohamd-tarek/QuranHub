import { Component, EventEmitter, Input, Output } from '@angular/core';
import { NotificationRepository } from '../../../abstractions/repositories/notificationRepository';

@Component({
  selector: "viewMoreNotifications",
  templateUrl: "viewMoreNotifications.component.html"
})

export class ViewMoreNotificationsComponent {

  constructor(public notificationRepository: NotificationRepository) {}

  onLoadMoreNotifications(event:any){ 
    event.stopPropagation();
    this.notificationRepository.loadMoreNotifications(10); 
  }
}
