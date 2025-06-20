import { InitStatusService } from './../service/init-status-service';
import { CanActivateFn } from '@angular/router';
import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { Observable, map } from 'rxjs';

import { inject } from '@angular/core';

export const startupRedirectGuard: CanActivateFn = (route, state) => {
  const initStatus = inject(InitStatusService);
  const router = inject(Router);

  return initStatus.isInitialized().pipe(
    map(initialized => {
      if (initialized) {
        router.navigate(['/']); // Сайт установлен → HomePage
      } else {
        router.navigate(['/install']); // Не установлен → Install
      }
      return false; // не пускаем на текущий маршрут (корень)
    })
  );
}