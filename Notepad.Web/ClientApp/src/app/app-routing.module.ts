import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AddDebtComponent } from './add-debt/add-debt.component';
import { NgModule } from '@angular/core';
import { DebtorHistoryComponent } from './debtor-history/debtor-history.component';
import { DeleteDebtComponent } from './delete-debt/delete-debt.component';

const routes: Routes = [
    {
        path: '',
        redirectTo: '/home',
        pathMatch: 'full'
    },
    {
        path: 'home',
        component: HomeComponent
    },
    {
        path: 'add-debt/:id',
        component: AddDebtComponent
    },
    {
        path: 'delete-debt/:id',
        component: DeleteDebtComponent
    },
    {
        path: 'history/:id',
        component: DebtorHistoryComponent
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule  { }