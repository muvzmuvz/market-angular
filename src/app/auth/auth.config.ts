import { ApplicationConfig } from '@angular/core';
import { provideAuth } from 'angular-auth-oidc-client';

export const authConfig: ApplicationConfig = {
  providers: [
    provideAuth({
      config: {
        authority: 'https://localhost:5042',
        redirectUrl: window.location.origin,
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
