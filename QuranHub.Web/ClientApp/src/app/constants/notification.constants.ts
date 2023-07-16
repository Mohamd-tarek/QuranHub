
import {BaseUrl, RestApiUrl } from "./application.constants";

export const NotificationUrl = RestApiUrl + "/Notification";

interface NotificationPathsType {

  readonly NotificationHub: string,
  readonly Recent: string;
  readonly GetNotificationById: string;
  readonly LoadMoreNotifications: string;
  readonly Seen: string;
  readonly Delete: string,
}

export const notificationPaths: NotificationPathsType = {
  NotificationHub: `${BaseUrl}/NotificationHub`,
  Recent: `${NotificationUrl}/Recent`,
  GetNotificationById: `${NotificationUrl}/GetNotificationById`,
  LoadMoreNotifications: `${NotificationUrl}/LoadMoreNotifications`,
  Seen: `${NotificationUrl}/Seen`,
  Delete: `${NotificationUrl}/Delete`,
};







