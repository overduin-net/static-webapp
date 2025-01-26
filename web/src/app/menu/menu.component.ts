import { Component } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';

export interface ClientPrincipal {
  identityProvider: string
  userId: string
  userDetails: string
  userRoles: string[]
}

@Component({
    selector: 'app-menu',
    templateUrl: './menu.component.html',
    styleUrls: ['./menu.component.scss'],
    standalone: false
})
export class MenuComponent {

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

  constructor(private breakpointObserver: BreakpointObserver, private http: HttpClient) {
    this.http.get('.auth/me').subscribe((resp: any)=> {
      this.user = resp.clientPrincipal;
    });
  }

  title = 'Template';
  public user: ClientPrincipal | undefined;


  isAuthenticated(){
    if(this.user && this.user.userRoles.some(x => x ==='authenticated'))
    {
      return true
    }
    return false;
  }

  isAdmin(){
    if(this.user && this.user.userRoles.some(x => x ==='administrator'))
    {
      return true
    }
    return false;
  }
  
  isReader(){
    return false;
  }


}
