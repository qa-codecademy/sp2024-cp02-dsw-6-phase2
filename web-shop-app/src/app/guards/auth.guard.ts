import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { UserService } from '../services/auth.services';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(UserService);
  const routerService = inject(Router);

  if(!authService.isLoggedIn()){
    return routerService.createUrlTree(['/login'])
  }
 
  return true;
};
