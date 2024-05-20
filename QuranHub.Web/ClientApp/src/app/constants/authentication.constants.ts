
import { RestApiUrl } from "./application.constants";

export const AuthenticatinUrl = RestApiUrl + "/authentication";

export const ExternalAuthenticatinUrl = RestApiUrl + "/external-authentication";

export const ReturnUrlType = 'returnUrl';

export const LogoutActions = {
  Logout: 'logout',
};

export const LoginActions = {
  LoginWithPassword: 'login-with-password',
  LoginWithExternalProvider: 'login-with-external-provider',
  LoginWithExternalProviderCallback: 'login-with-external-provider-callback',
  LoginFailed: 'login-failed',
};

export const SignupActions = {
  Signup: 'signup',
  SignupCallback: 'signup-callback',
  SignupWithExternalProvider: 'signup-with-external-provider',
  SignupWithExternalProviderCallback: 'signup-with-external-provider-callback',
  SignupConfirm: 'signup-confirm',
  SignupResend: 'signup-resend',
  SignupFailed: 'signup-failed',
};

interface IdentityPathsType {
  readonly LoginWithPassword: string;
  readonly LoginWithExternalProvider: string;
  readonly LoginWithExternalProviderCallback: string;
  readonly Signup: string;
  readonly SignupWithExternalProvider: string;
  readonly SignupWithExternalProviderCallback: string;
  readonly SignupConfirm: string;
  readonly SignupResend: string;
  readonly SignupCallback: string;
  readonly LogOut: string;
  readonly ExternalSchemas: string,
  readonly RecoverPassword: string,
}

export const identityPaths: IdentityPathsType = {
    LoginWithPassword: `${AuthenticatinUrl}/${LoginActions.LoginWithPassword}`,
    LoginWithExternalProvider: `${ExternalAuthenticatinUrl}/${LoginActions.LoginWithExternalProvider}`,
    LoginWithExternalProviderCallback: `${ExternalAuthenticatinUrl}/${LoginActions.LoginWithExternalProviderCallback}`,
    Signup: `${AuthenticatinUrl}/${SignupActions.Signup}`,
    SignupCallback: `${AuthenticatinUrl}/${SignupActions.SignupCallback}`,
    SignupWithExternalProvider: `${ExternalAuthenticatinUrl}/${SignupActions.SignupWithExternalProvider}`,
    SignupWithExternalProviderCallback: `${ExternalAuthenticatinUrl}/${SignupActions.SignupWithExternalProviderCallback}`,
    SignupConfirm: `${AuthenticatinUrl}/${SignupActions.SignupConfirm}`,
    SignupResend: `${AuthenticatinUrl}/${SignupActions.SignupResend}`,
    LogOut: `${AuthenticatinUrl}/${LogoutActions.Logout}`,
    ExternalSchemas: `${ExternalAuthenticatinUrl}/external-schemas`,
    RecoverPassword: `${AuthenticatinUrl}/recover-password`,
};







