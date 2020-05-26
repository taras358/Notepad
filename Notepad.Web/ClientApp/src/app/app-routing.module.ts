import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { DebtorHistoryComponent } from './debtor-history/debtor-history.component';
import { AddDebtComponent } from './add-debt/add-debt.component';
import { DeleteDebtComponent } from './delete-debt/delete-debt.component';
import { NgModule } from '@angular/core';
import { CanActivateDebtor } from './shared/guards/can-activete-debtor.guard';
import { DownloadReportComponent } from './download-report/download-report.component';


const routes: Routes = [
    {
        path: '',
        redirectTo: 'home',
        pathMatch: 'full'
    },
    {
        path: 'home',
        component: HomeComponent
    },
    {
        path: 'history',
        component: DebtorHistoryComponent,
        canActivate: [CanActivateDebtor]
    },
    {
        path: 'add-debt',
        component: AddDebtComponent,
        canActivate: [CanActivateDebtor]
    },
    {
        path: 'delete-debt',
        component: DeleteDebtComponent,
        canActivate: [CanActivateDebtor]
    },
    {
        path: 'download',
        component: DownloadReportComponent,
        canActivate: [CanActivateDebtor]
    },
    {
        path: '**',
        redirectTo: 'home'
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }