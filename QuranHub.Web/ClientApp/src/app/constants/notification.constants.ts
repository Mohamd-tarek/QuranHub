
import {BaseUrl, RestApiUrl } from "./application.constants";

export const NotificationUrl = RestApiUrl + "/notification";

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
  Recent: `${NotificationUrl}/recent`,
  GetNotificationById: `${NotificationUrl}/get-notification-by-id`,
  LoadMoreNotifications: `${NotificationUrl}/load-more-notifications`,
  Seen: `${NotificationUrl}/seen`,
  Delete: `${NotificationUrl}/delete`,
};







