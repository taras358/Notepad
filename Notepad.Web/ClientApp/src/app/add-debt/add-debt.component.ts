import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CreateDebtRequest } from '../shared/models/create-debt-request';
import { DebtService } from '../shared/services/debt.service';
import { NavbarService } from '../shared/services/nav-bar.service';
import { Debtor } from '../shared/models/deptor';
import { DebtorService } from '../shared/services/debtor.service';

@Component({
  selector: 'app-add-debt',
  templateUrl: './add-debt.component.html',
  styleUrls: ['./add-debt.component.scss']
})
export class AddDebtComponent implements OnInit {

  public addDebtGroup: FormGroup;
  public debtorId: string;
  public debtor: Debtor;

  constructor(private route: ActivatedRoute,
    private debtService: DebtService,
    private router: Router,
    private navbarService: NavbarService,
    private debtorService: DebtorService) { }

  ngOnInit() {
    const debtor = this.debtorService.getCurrentDebtor();
    if (debtor) {
      this.debtor = debtor;
      this.debtorId = debtor.id;
    }
    this.navbarService.updateLinks();

    this.addDebtGroup = new FormGroup({
      amount: new FormControl(null, Validators.required),
      description: new FormControl('', Validators.required)
    });
  }


  public onAddDebtClick() {
    const amount: number = parseFloat(this.addDebtGroup.controls.amount.value);
    const description: string = this.addDebtGroup.controls.description.value;
    const newDebt = {
      debtorId: this.debtorId,
      amount: amount,
      description: description
    } as CreateDebtRequest;
    this.debtService.createDebt(newDebt)
      .subscribe(response => {
        this.router.navigate(['/history']);
      });

  }

}
