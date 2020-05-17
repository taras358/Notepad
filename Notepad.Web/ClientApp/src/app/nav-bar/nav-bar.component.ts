import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NavbarService } from '../shared/services/nav-bar.service';
import { DebtorHistoryComponent } from '../debtor-history/debtor-history.component';
import { AddDebtComponent } from '../add-debt/add-debt.component';
import { DeleteDebtComponent } from '../delete-debt/delete-debt.component';
import { Debtor } from '../shared/models/deptor';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {

  public links: Array<{ text: string, path: string, iconClass: string}>;
  public currenctDebtor: Debtor;

  constructor(private router: Router,
    private navbarService: NavbarService) {
    this.router.config.unshift(
      { path: 'history', component: DebtorHistoryComponent },
      { path: 'add-debt', component: AddDebtComponent },
      { path: 'delete-debt', component: DeleteDebtComponent },
    );
  }

  ngOnInit() {
    this.links = this.navbarService.getLinks();
    this.navbarService.getDebtor()
      .subscribe(debtor => this.currenctDebtor = debtor);
  }

  backToMain() {
    this.navbarService.removeDebtor();
    this.links = this.navbarService.getLinks();
    this.router.navigate(['home']);
  }
}
