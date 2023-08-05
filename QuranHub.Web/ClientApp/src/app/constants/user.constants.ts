
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
    ChangePassword: `${UserUrl}/ChangePassword`,
    AboutInfo: `${UserUrl}/AboutInfo`,
    EditAboutInfo: `${UserUrl}/EditAboutInfo`,
    DeleteAccount: `${UserUrl}/DeleteAccount`,
    EditUserInfo: `${UserUrl}/EditUserInfo`,
    UserInfo: `${UserUrl}/UserInfo`,
    PrivacySetting: `${UserUrl}/PrivacySetting`,
};







