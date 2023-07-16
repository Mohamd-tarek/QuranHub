import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { HttpClient, HttpParams} from "@angular/common/http";
import { NotificationRepository } from "src/app/abstractions/repositories/notificationRepository";
import { notificationPaths } from "../../constants/notification.constants";
import * as signalR from '@microsoft/signalr';  
import { Notification } from "../notification.model";
import { AuthenticationService } from "src/app/abstractions/services/authenticationService";

@Injectable({
  providedIn: 'root',
})

export class NotifcationDataRepository extends NotificationRepository  {

  notifications:Notification[] = [];
  endOfNotifications: boolean = false;

  constructor( private http: HttpClient,private authService:AuthenticationService) {
    super();
    this.getNotifications();
     this.openConnection();
  }

  openConnection(): void
  {
    let authToken = this.authService.getAuthenticationToken();

    const connection = new signalR.HubConnectionBuilder()  
    .configureLogging(signalR.LogLevel.Information)  
    .withUrl(notificationPaths.NotificationHub, { accessTokenFactory: () => authToken })  
    .build();  

    connection.start().then(function () {  
      console.log('notificationHub started');  
    }).catch(function (err) {  
      return console.error(err.toString());  
    });  

    connection.on("RecieveNotification", (notification) => {  
      this.receiveNotification(notification);  
    });  

  }

  getNotificationById(notificationId:number): Observable<Notification> 
  {
    return this.http.get<Notification>(notificationPaths.GetNotificationById + '/' + notificationId)
  }

  getNotifications(): void
  {
    this.http.get<Notification[]>(notificationPaths.Recent).subscribe(response => {
      this.notifications = response
      console.log(response);  
    });
  }

  loadMoreNotifications(size: number): void
  {
    let offset = this.notifications.length;

    this.http.get<Notification[]>(notificationPaths.LoadMoreNotifications + '/' + offset + '/' + size).subscribe(response => {
      if(response.length == 0)
      {
        this.endOfNotifications = true;
      }
      else
      {
        this.notifications.push(...response);
      }
    });
  }

  receiveNotification(notification :Notification ): void
  {
     this.notifications.unshift(notification)
  }

  markNotificationAsSeen(notificationId: number): Observable<any>
  {
    return this.http.get(notificationPaths.Recent + '/' + notificationId);
  }

  deleteNotification(notificationId: number): Observable<any>
  {
    let httpParams = new HttpParams().set('notificationId', notificationId);
    let options = { params: httpParams };

    return this.http.delete(notificationPaths.Delete, options);
  }

}
