
import { RestApiUrl } from "./application.constants";

export const AuthenticatinUrl = RestApiUrl + "/authentication";

export const ExternalAuthenticatinUrl = RestApiUrl + "/ExternalAuthentication";

export const ReturnUrlType = 'returnUrl';

export const LogoutActions = {
  Logout: 'Logout',
};

export const LoginActions = {
  LoginWithPassword: 'LoginWithPassword',
  LoginWithExternalProvider: 'LoginWithExternalProvider',
  LoginWithExternalProviderCallback: 'LoginWithExternalProviderCallback',
  LoginFailed: 'LoginFailed',
};

export const SignupActions = {
  Signup: 'Signup',
  SignupCallback: 'SignupCallback',
  SignupWithExternalProvider: 'SignupWithExternalProvider',
  SignupWithExternalProviderCallback: 'SignupWithExternalProviderCallback',
  SignupConfirm: 'SignupConfirm',
  SignupResend: 'SignupResend',
  SignupFailed: 'SignupFailed',
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
    ExternalSchemas: `${ExternalAuthenticatinUrl}/externalSchemas`,
    RecoverPassword: `${AuthenticatinUrl}/recoverPassword`,
};







