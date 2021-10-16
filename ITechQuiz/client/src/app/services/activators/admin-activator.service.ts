import {Injectable} from '@angular/core';
import {AuthService} from "../auth.service";
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from "@angular/router";
import {Observable} from "rxjs";

@Injectable()
export class AdminActivatorService implements CanActivate {

    constructor(private authService: AuthService, private router: Router) {

    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):
        Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
        if (!this.authService.isAdmin) {
            this.router.navigate(["login"])
            return false
        } else return true
    }

}
