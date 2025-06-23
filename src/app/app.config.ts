import { provideEventPlugins } from "@taiga-ui/event-plugins";
import { provideAnimations } from "@angular/platform-browser/animations";
import { ApplicationConfig, provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withFetch } from '@angular/common/http';
import { provideAuth } from 'angular-auth-oidc-client';
import { routes } from './app.routes';
import { provideClientHydration, withEventReplay } from '@angular/platform-browser';

// Без редиректа! Просто возвращаем адрес приложения
const getRedirectUrl = () => {
  if (typeof window !== 'undefined') {
    return window.location.origin; // http://localhost:4200
  }
  return 'http://localhost:4200'; // fallback
};

export const appConfig: ApplicationConfig = {
  providers: [
    provideAnimations(),
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideClientHydration(withEventReplay()),
    provideEventPlugins(),
    provideHttpClient(withFetch()),
    provideAuth({
      config: {
        authority: 'http://localhost:5042',
        redirectUrl: getRedirectUrl(), // Без ручного перехода
        postLogoutRedirectUri: 'http://localhost:4200',
        clientId: 'web',
        scope: 'openid profile roles api',
        responseType: 'code',
        silentRenew: true,
        useRefreshToken: true,
      },
    }),
  ],
};
