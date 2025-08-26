import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, GuardResult, MaybeAsync, Router, RouterStateSnapshot } from '@angular/router';
import { AuthService } from "../service/auth.service";

@Injectable({
    providedIn: 'root'
  })

  export class AuthGuard implements CanActivate {
    constructor(private authService: AuthService, private router: Router) {}
  
    canActivate(): boolean {
      if (!this.authService.isTokenExpired()) {
        return true;
      }
      this.router.navigate(['/login']);
      return false;
    }
  }