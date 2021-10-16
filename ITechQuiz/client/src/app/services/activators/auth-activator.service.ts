import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from "@angular/router";
import {Observable} from "rxjs";
import {Injectable} from "@angular/core";
import {AuthService} from "../auth.service";

@Injectable()
export class AuthActivatorService implements CanActivate{
    
    constructor(private authService: AuthService, private router: Router) {
        
    }
    
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):
        Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree 
    {
        if (this.authService.loginRequired)
        {
            this.router.navigate(["login"])
            return false
        }
        else return true
    }
    
}