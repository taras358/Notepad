import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class NavbarService {
    private links = new Array<{ text: string, path: string, iconClass: string }>();
    private isDebtorSelected = new Subject<boolean>();

    constructor() {
        this.isDebtorSelected.next(false);
    }
    public getLinks() {
        return this.links;
    }
    public getDebtorStatus() {
        return this.isDebtorSelected;
    }

    public updateDebtorStatus(status: boolean) {
        this.isDebtorSelected.next(status);
        if (!status) {
            this.clearAllItems();
        }
    }
    public updateNavAfterDebtorSelect(): void {
        this.addItem({ text: 'History', path: 'history/:id', iconClass: 'icon-history-plus' });
        this.addItem({ text: 'Add debt', path: 'add-debt/:id', iconClass: 'icon-add-sign' });
        this.addItem({ text: 'Delete debt', path: 'delete-debt/:id', iconClass: 'icon-delete-sign' });
    }
    public addItem({ text, path, iconClass }) {
        this.links.push({ text: text, path: path, iconClass: iconClass });
    }
    public removeItem({ text }) {
        this.links.forEach((link, index) => {
            if (link.text === text) {
                this.links.splice(index, 1);
            }
        });
    }
    public clearAllItems() {
        this.links.length = 0;
    }
}