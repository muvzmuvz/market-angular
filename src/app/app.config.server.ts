import { mergeApplicationConfig, ApplicationConfig } from '@angular/core';
import { provideServerRendering, withRoutes } from '@angular/ssr';
import { provideAuth } from 'angular-auth-oidc-client';

import { appConfig } from './app.config';
import { serverRoutes } from './app.routes.server';

// Конфигурация OIDC для SSR (без использования window)
const authConfigServer: ApplicationConfig = {
  providers: [
    provideAuth({
      config: {
        authority: 'http://localhost:5042', // ← замени на своего провайдера
        redirectUrl: 'http://localhost:4200',             // ← адрес клиента (SSR)
        postLogoutRedirectUri: 'http://localhost:4200',
        clientId: 'web',                       // ← client_id из настроек провайдера
        scope: 'openid profile roles api offline_access',
        responseType: 'code',
        silentRenew: false,       // SSR не поддерживает silent renew
        useRefreshToken: false    // refresh токены тоже не работают на сервере
      },
    }),
  ],
};

// SSR конфиг + роутинг
const serverConfig: ApplicationConfig = {
  providers: [
    provideServerRendering(withRoutes(serverRoutes))
  ]
};

// Объединяем клиентскую, серверную и auth конфигурации
export const config = mergeApplicationConfig(
  appConfig,
  serverConfig,
  authConfigServer
);
