import { ApplicationConfig } from '@angular/core';
import { provideAuth } from 'angular-auth-oidc-client';

export const authConfig: ApplicationConfig = {
  providers: [
    provideAuth({
      config: {
        authority: 'http://localhost:5042',
        redirectUrl: window.location.href = 'http://localhost:5042/Authorize/Login?returnUrl=' + encodeURIComponent(window.location.href),
        postLogoutRedirectUri: window.location.origin,
        clientId: 'web',
        scope: 'openid profile roles api offline_access',
        responseType: 'code',
        silentRenew: true,
        useRefreshToken: true,
      },
    }),
  ],
};
