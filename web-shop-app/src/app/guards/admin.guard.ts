import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { UserService } from '../services/auth.services';

export const adminGuard: CanActivateFn = (route, state) => {
  const authService = inject(UserService);
  const routerService = inject(Router);

  if(!authService.isAdmin()){
    return routerService.createUrlTree(['/not-allowed'])
  }
  return true;
};
