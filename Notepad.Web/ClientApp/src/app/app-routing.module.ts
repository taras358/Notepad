import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { AuthGuard } from './shared/guards/auth.guard';


const routes: Routes = [
    {
        path: 'dashboard',
        canActivate: [AuthGuard],
        loadChildren: () => import('./dashboard/dashboard.module').then(m => m.DashboardModule)
    },
    {
        path: 'auth',
        loadChildren: () => import('./auth/auth.module').then(m => m.AuthModule)
    },
    {
        path: '', redirectTo: 'dashboard', pathMatch: 'full'
    },
    {
        path: '**', redirectTo: 'dashboard'
    }
];

// const routes: Routes = [
//     {
//         path: '',
//         redirectTo: 'home',
//         pathMatch: 'full'
//     },
//     {
//         path: 'home',
//         component: HomeComponent
//     },
//     {
//         path: 'history',
//         component: DebtorHistoryComponent,
//         canActivate: [CanActivateDebtor]
//     },
//     {
//         path: 'add-debt',
//         component: AddDebtComponent,
//         canActivate: [CanActivateDebtor]
//     },
//     {
//         path: 'delete-debt',
//         component: DeleteDebtComponent,
//         canActivate: [CanActivateDebtor]
//     },
//     {
//         path: 'download',
//         component: DownloadReportComponent,
//         canActivate: [CanActivateDebtor]
//     },
//     {
//         path: '**',
//         redirectTo: 'home'
//     }
// ];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }