import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './shared/guards/auth.guard';


const routes: Routes = [
  { path: '', redirectTo: '', pathMatch: 'full' },
  // {
  //   path: 'auth',
  //   loadChildren: () => import('./authentication/authentication.modules').then(m => m.AuthenticationModule)
  // },
  {
    path: '',
    // canActivate: [AuthGuard],
    loadChildren: () => import('./main/main.module').then(m => m.MainModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
