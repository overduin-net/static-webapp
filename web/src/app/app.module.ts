import { APP_INITIALIZER, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { provideHttpClient, withInterceptorsFromDi } from "@angular/common/http";
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';

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
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatSidenavModule } from '@angular/material/sidenav';

import { LayoutModule } from '@angular/cdk/layout';

import { MenuComponent } from './menu/menu.component';
import { HomeComponent } from './home/home.component';
import { PersonComponent } from './person/person.component';

import { AuthService } from 'src/services/auth.service';
import { ApiService, API_URL } from 'src/services/generated/api.service';
import { AccountService } from 'src/services/account.service';

@NgModule({ declarations: [
        AppComponent,
        PersonComponent,
        HomeComponent,
        MenuComponent
    ],
    bootstrap: [AppComponent], imports: [BrowserModule,
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
        MatPaginatorModule,
        MatSortModule,
        FormsModule,
        LayoutModule,
        MatSidenavModule], providers: [
        { provide: API_URL, useValue: getOrigin(), deps: [APP_INITIALIZER] },
        AccountService,
        AuthService,
        ApiService,
        provideHttpClient(withInterceptorsFromDi())
    ] })
export class AppModule { }

function getOrigin(): string {
  return window.location.origin;
}

