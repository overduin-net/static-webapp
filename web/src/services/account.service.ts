import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';

export interface UserDetails {
  clientPrincipal: ClientPrincipal
}

export interface ClientPrincipal {
  identityProvider: string
  userId: string
  userDetails: string
  userRoles: string[]
}

@Injectable({
  providedIn: 'root'
})

export class AccountService {
    public userDetails: UserDetails;
    constructor(private http: HttpClient) {
      this.userDetails = this.login();
    }
    
    login() : any{        
        return this.http.get('/.auth/me');  
      }

}