import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NavbarService } from '../shared/services/nav-bar.service';
import { DebtorHistoryComponent } from '../debtor-history/debtor-history.component';
import { AddDebtComponent } from '../add-debt/add-debt.component';
import { DeleteDebtComponent } from '../delete-debt/delete-debt.component';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {

  public links: Array<{ text: string, path: string, iconClass: string}>;
  public isLoggedIn = false;

  constructor(private router: Router,
    private navbarService: NavbarService) {
    this.router.config.unshift(
      { path: 'history/:id', component: DebtorHistoryComponent },
      { path: 'add-debt/:id', component: AddDebtComponent },
      { path: 'delete-debt/:id', component: DeleteDebtComponent },
    );
  }

  ngOnInit() {
    this.navbarService.updateNavAfterDebtorSelect();
    this.links = this.navbarService.getLinks();
    this.navbarService.getDebtorStatus()
      .subscribe(status => this.isLoggedIn = status);
  }
  backToMain() {
    this.navbarService.updateDebtorStatus(false);
    this.router.navigate(['home']);
  }
}
