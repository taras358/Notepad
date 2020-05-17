import { Component, OnInit } from '@angular/core';
import { DebtorService } from '../shared/services/debtor.service';
import { DebtorResponse } from '../shared/models/debtor-response.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { DebtorRequest } from '../shared/models/debtor-request.model';
import { Debtor } from '../shared/models/deptor';
import { Router } from '@angular/router';
import { NavbarService } from '../shared/services/nav-bar.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  public deptors: Array<Debtor>;
  public filtered: Array<Debtor>;
  public isNotFound: boolean;

  constructor(public debtorServise: DebtorService,
    private router: Router,
    private navbarService: NavbarService) { }

  public createDeptorFrom: FormGroup;
  public serachDeptorForm: FormGroup;


  ngOnInit() {
    this.deptors = [];
    this.filtered = [];
    this.isNotFound = false;
    this.getGebtors();

    this.createDeptorFrom = new FormGroup({
      name: new FormControl('', Validators.required),
      surname: new FormControl('', Validators.required)
    });
    this.serachDeptorForm = new FormGroup({
      fullName: new FormControl('', Validators.required)
    });
  }

  public onDebtorClick(debtor: Debtor) {
    if (debtor) {
      this.navbarService.setDebtor(debtor);
      this.router.navigate(['/history']);
    }
  }

  public onAddDebtorClick() {
    const newDebtor = {
      name: this.createDeptorFrom.controls.name.value,
      surname: this.createDeptorFrom.controls.surname.value
    } as DebtorRequest;
    this.debtorServise.createDebtor(newDebtor).subscribe(response => {
      this.getGebtors();
    });
  }


  public onSearchDebtorClick() {
    const query = this.serachDeptorForm.controls.fullName.value;
    this.findDebtors(query);
  }

  public getGebtors(): void {
    this.debtorServise.getGebtors()
      .subscribe(response => {
        this.deptors = response.debtors.map((x: DebtorResponse) => {
          return {
            id: x.id,
            name: x.name,
            surname: x.surname,
            totalDebt: x.totalDebt
          } as Debtor;
        });
      });
  }

  private findDebtors(query: string): void {
    if (query) {
      this.debtorServise.findDebtors(query)
        .subscribe(response => {
          this.filtered = response.debtors.map((x: DebtorResponse) => {
            return {
              id: x.id,
              name: x.name,
              surname: x.surname,
              totalDebt: x.totalDebt
            } as Debtor;
          });
          if (this.filtered.length === 0) {
            this.isNotFound = true;
          } else {
            this.isNotFound = false;
          }
        });
    }
  }
}


