import { provideEventPlugins } from "@taiga-ui/event-plugins";
import { provideAnimations } from "@angular/platform-browser/animations";
import { ApplicationConfig, provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { provideHttpClient, withFetch } from '@angular/common/http';
import { provideAuth } from 'angular-auth-oidc-client';



import { routes } from './app.routes';
import { provideClientHydration, withEventReplay } from '@angular/platform-browser';
const getRedirectUrl = () => {
  if (typeof window !== 'undefined') {
    return window.location.origin;
  }
  return 'http://localhost:4200';  // или любой дефолтный URL для сервера
};
export const appConfig: ApplicationConfig = {
  providers: [provideAnimations(), provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes), provideClientHydration(withEventReplay()), provideEventPlugins(), provideEventPlugins(), provideHttpClient(withFetch()),
    provideAuth({
      config: {
        authority: 'https://localhost:5042',
        redirectUrl: getRedirectUrl(),
        postLogoutRedirectUri: getRedirectUrl(),
        clientId: 'web',
        scope: 'openid profile roles api offline_access',
        responseType: 'code',
        silentRenew: true,
        useRefreshToken: true,
      },
    }),
  ],
};
