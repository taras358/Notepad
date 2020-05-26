import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NavbarService } from '../shared/services/nav-bar.service';
import { Debtor } from '../shared/models/deptor';
import { DebtorService } from '../shared/services/debtor.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {

  public links: Array<{ text: string, path: string, iconClass: string }>;
  public currenctDebtor: Debtor;

  constructor(private router: Router,
    private navbarService: NavbarService,
    private debtorService: DebtorService) { }

  ngOnInit() {
    // this.currenctDebtor = this.debtorService.getCurrentDebtor();

    this.links = this.navbarService.getLinks();

  }

  backToMain() {
    this.debtorService.removeDebtor();
    this.links = this.navbarService.getLinks();
    this.router.navigate(['home']);
  }
  public onNavigateClick(link) {
    this.router.navigate([link.path]);
  }
}
