import { APP_INITIALIZER, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from "@angular/common/http"; // Import 

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatListModule } from '@angular/material/list';
import { MatSliderModule } from '@angular/material/slider';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatMenuModule } from '@angular/material/menu';
import { MatTableModule } from '@angular/material/table';

import { HomeComponent } from './home/home.component';
import { AccountService } from 'src/services/account.service';
import { AuthService } from 'src/services/auth.service';
import { ApiService, API_URL } from 'src/services/generated/api.service';
import { PersonComponent } from './person/person.component';
import { LayoutModule } from '@angular/cdk/layout';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MenuComponent } from './menu/menu.component';
import { environment } from 'src/environments/environment';

@NgModule({
  declarations: [
    AppComponent,
    PersonComponent,
    HomeComponent,
    MenuComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatToolbarModule,
    MatListModule,
    MatSliderModule,
    MatCardModule,
    MatIconModule,
    MatToolbarModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatTooltipModule,
    MatMenuModule,
    MatTableModule,
    HttpClientModule,
    FormsModule,
    LayoutModule,
    MatSidenavModule
  ],
  providers: [
    { provide: API_URL, useValue: getOrigin(), deps: [APP_INITIALIZER] },
    AccountService,
    AuthService,
    ApiService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

function getOrigin(): string {
  return window.location.origin;
}

