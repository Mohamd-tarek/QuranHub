import { Observable } from "rxjs";
import { Notification } from "../../models/notification.model";

export abstract  class NotificationRepository{

  notifications:any;

  endOfNotifications:any;

  abstract openConnection(): void;

  abstract getNotificationById(notificationId: number): Observable<Notification>;

  abstract getNotifications(): void;

  abstract loadMoreNotifications(size: number): void;

  abstract receiveNotification(notification :Notification ): void;

  abstract markNotificationAsSeen(notificationId: number): Observable<any>;

  abstract deleteNotification(notificationId: number): Observable<any>;

}
