import { InitStatusService } from './../../service/init-status-service';
import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { Observable, map } from 'rxjs';

export const initGuard: CanActivateFn = (route, state) => {
  const initStatusService = inject(InitStatusService);
  const router = inject(Router);

  return initStatusService.isInitialized().pipe(
    map(initialized => {
      if (initialized) {
        router.navigate(['/']);
        return false;
      }
      return true;
    })
  );
};