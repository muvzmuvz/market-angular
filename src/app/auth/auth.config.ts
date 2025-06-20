import { OpenIdConfiguration } from 'angular-auth-oidc-client';

export const authConfig: OpenIdConfiguration = {
  authority: 'http://localhost:5042',
  clientId: 'web',
  redirectUrl: window ? window.location.origin + '/callback' : '', // Обработка SSR
  postLogoutRedirectUri: window ? window.location.origin : '',
  scope: 'openid profile roles api offline_access',
  responseType: 'code',
  silentRenew: true,
  useRefreshToken: true,
  renewTimeBeforeTokenExpiresInSeconds: 30,
};