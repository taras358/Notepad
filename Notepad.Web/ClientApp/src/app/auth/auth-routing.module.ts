import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { NgModule } from '@angular/core';
import { AuthComponent } from './auth.component';

const routes: Routes = [
  {
    path: '',
    component: AuthComponent,
    children: [
      {
        path: 'login',
        component: LoginComponent
      },
      { path: '', redirectTo: 'login', pathMatch: 'full' },
      { path: '**', component: LoginComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule { }
