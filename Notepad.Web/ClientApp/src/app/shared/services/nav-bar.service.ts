import { Injectable } from '@angular/core';
import { DebtorService } from './debtor.service';

@Injectable({
    providedIn: 'root'
})
export class NavbarService {
    private links = new Array<{ text: string, path: string, iconClass: string }>();

    constructor(private debtorServise: DebtorService) { }
    public getLinks() {
        return this.links;
    }

    // public setDebtor(debtor: Debtor) {
    //     this.currentDeptorSubject.next(debtor);
    //     localStorage.setItem('debtor', JSON.stringify(debtor));
    //     if (debtor) {
    //         this.updateNavAfterDebtorSelect();
    //     }
    // }

    // public removeDebtor() {
    //     this.currentDeptorSubject.next(null);
    //     localStorage.removeItem('debtor');
    //     this.clearAllItems();
    // }

    public updateLinks(): void {
        this.clearAllItems();
        if (this.debtorServise.isDebtorExist()) {
            this.addItem({ text: 'History', path: 'history', iconClass: 'icon-history-plus' });
            this.addItem({ text: 'Add debt', path: 'add-debt', iconClass: 'icon-add-sign' });
            this.addItem({ text: 'Delete debt', path: 'delete-debt', iconClass: 'icon-delete-sign' });
        } else  {
            this.addItem({ text: 'Home', path: 'home', iconClass: 'icon-search' });
        }
    }

    public addItem({ text, path, iconClass }) {
        this.links.push({ text: text, path: path, iconClass: iconClass });
    }

    public clearAllItems() {
        this.links.length = 0;
    }
}
