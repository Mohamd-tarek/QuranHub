
import { RestApiUrl } from "./application.constants";

export const UserUrl = RestApiUrl + "/account";

export const ReturnUrlType = '/returnUrl';

interface UserPathsType {

  readonly ChangePassword: string,
  readonly AboutInfo: string;
  readonly EditAboutInfo: string;
  readonly DeleteAccount: string,
  readonly EditUserInfo: string,
  readonly UserInfo: string,
  readonly PrivacySetting: string
}

export const userPaths: UserPathsType = {
    ChangePassword: `${UserUrl}/change-password`,
    AboutInfo: `${UserUrl}/about-info`,
    EditAboutInfo: `${UserUrl}/edit-about-info`,
    DeleteAccount: `${UserUrl}/delete-account`,
    EditUserInfo: `${UserUrl}/edit-user-info`,
    UserInfo: `${UserUrl}/user-info`,
    PrivacySetting: `${UserUrl}/privacy-setting`,
};







