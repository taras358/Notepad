import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { DebtorService } from '../services/debtor.service';

@Injectable({
    providedIn: 'root'
})
export class CanActivateDebtor implements CanActivate {
    constructor(private debtorDervice: DebtorService) { }

    canActivate(): boolean {
        return this.debtorDervice.isDebtorExist();
    }
}
