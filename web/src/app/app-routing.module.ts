import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthService } from 'src/services/auth.service';
import { HomeComponent } from './home/home.component';
import { PersonComponent } from './person/person.component';

const routes: Routes = [
  {
    path: 'person',
    component: PersonComponent,
    canActivate: [AuthService]
  },
  {
    path: 'home',
    component: HomeComponent
  },
  {
    path: '',
    component: HomeComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
