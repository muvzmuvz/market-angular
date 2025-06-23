import { mergeApplicationConfig, ApplicationConfig } from '@angular/core';
import { provideServerRendering, withRoutes } from '@angular/ssr';
import { provideAuth } from 'angular-auth-oidc-client';

import { appConfig } from './app.config';
import { serverRoutes } from './app.routes.server';

// Конфигурация OIDC для SSR (без использования window)

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

);
