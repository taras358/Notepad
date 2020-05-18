import { Injectable } from '@angular/core';
import { Subject, BehaviorSubject, Observable } from 'rxjs';
import { Debtor } from '../models/deptor';

@Injectable({
    providedIn: 'root'
})
export class NavbarService {
    private links = new Array<{ text: string, path: string, iconClass: string }>();
    private isDebtorSelected = new Subject<boolean>();

    private currentDeptorSubject: BehaviorSubject<Debtor>;
    private currentDeptor: Observable<Debtor>;

    constructor() {
        this.isDebtorSelected.next(false);
        this.currentDeptorSubject = new BehaviorSubject<Debtor>(JSON.parse(localStorage.getItem('debtor')));
        this.currentDeptor = this.currentDeptorSubject.asObservable();
    }
    public getLinks() {
        return this.links;
    }
    public getDebtor() {
        return this.currentDeptor;
    }

    public setDebtor(debtor: Debtor) {
        this.currentDeptorSubject.next(debtor);
        localStorage.setItem('debtor', JSON.stringify(debtor));
        if (debtor) {
            this.updateNavAfterDebtorSelect();
        }
    }
    public removeDebtor() {
        this.currentDeptorSubject.next(null);
        localStorage.removeItem('debtor');
        this.clearAllItems();
    }

    private updateNavAfterDebtorSelect(): void {
        this.clearAllItems();
        this.addItem({ text: 'History', path: 'history', iconClass: 'icon-history-plus' });
        this.addItem({ text: 'Add debt', path: 'add-debt', iconClass: 'icon-add-sign' });
        this.addItem({ text: 'Delete debt', path: 'delete-debt', iconClass: 'icon-delete-sign' });
    }

    public addItem({ text, path, iconClass }) {
        this.links.push({ text: text, path: path, iconClass: iconClass });
    }

    private clearAllItems() {
        this.links.length = 0;
    }
    public updateLinks() {
        this.updateNavAfterDebtorSelect();
      }
}
