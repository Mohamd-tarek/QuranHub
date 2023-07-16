import { Component, Input } from '@angular/core';
import { NotificationRepository } from '../../../abstractions/repositories/notificationRepository';
import { Notification } from 'src/app/models/notification.model';
import { Router }   from "@angular/router";
import { FadeOutTrigger } from 'src/app/animations/FadeOut.animation';

@Component({
  selector: 'notification',
  templateUrl: './notification.component.html',
  animations: [FadeOutTrigger]
})

export class NotificationComponent {
  
  @Input()
  notification!:Notification;

  submitted:boolean = false;

  constructor(
    private router: Router,
    public notificationRepository: NotificationRepository) {
  }

  viewNotification()
  {
    this.notificationRepository.markNotificationAsSeen(this.notification.notificationId).subscribe(()=>{
      this.notification.seen = true;
    });

    if(this.notification.type == "FollowNotification")
    {
      this.router.navigateByUrl("/profile/" + this.notification.sourceUser.id + "/profileDetails");
    }
    else
    {
      this.router.navigateByUrl("/postViewer/" + this.notification.notificationId);
    }

  }

  deleteNotification(event:any)
  {
    event.stopPropagation();
    console.log(this.notification.notificationId);
    this.submitted = true;
    this.notificationRepository.deleteNotification(this.notification.notificationId).subscribe(result => {
        this.submitted = false;
      for (let i = 0; i < this.notificationRepository.notifications.length; ++i)
        {
        if (this.notificationRepository.notifications[i].notificationId == this.notification.notificationId)
          {
          this.notificationRepository.notifications.splice(i, 1);
          }

        }
    },
    ()=> {
      this.submitted = false;
    })
  }
}
