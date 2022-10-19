import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class AuthService implements CanActivate {

    constructor(private router: Router, private http: HttpClient) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
        return this.validateLogin();
    }

    validateLogin(): any {
        this.http.get('/.auth/me').subscribe((x: any) => {
            //console.log("IsAuthenticated?? : " + JSON.stringify(x));
            if (x && x.clientPrincipal) {
                return true;
            }
            window.location.replace('/.auth/login/aad');
            return false;
        });
    }
}