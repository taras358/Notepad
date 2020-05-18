import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DebtService } from '../shared/services/debt.service';
import { CreateDebtRequest } from '../shared/models/create-debt-request';
import { DeleteDebtRequest } from '../shared/models/delete-debt-request';
import { NavbarService } from '../shared/services/nav-bar.service';

@Component({
  selector: 'app-delete-debt',
  templateUrl: './delete-debt.component.html',
  styleUrls: ['./delete-debt.component.scss']
})
export class DeleteDebtComponent implements OnInit {

  public deleteDebtGroup: FormGroup;
  public debtorId: string;

  constructor(private route: ActivatedRoute,
    private debtService: DebtService,
    private router: Router,
    private navbarService: NavbarService) { }

  ngOnInit() {
    this.navbarService.getDebtor()
      .subscribe(debtor => {
        if (debtor) {
          this.debtorId = debtor.id;
          this.navbarService.updateLinks();
        }
      });

    this.deleteDebtGroup = new FormGroup({
      amount: new FormControl(0, Validators.required)
    });
  }


  public onDeleteDebtClick() {
    const amount: number = parseFloat(this.deleteDebtGroup.controls.amount.value);
    const newDebt = {
      debtorId: this.debtorId,
      amount: amount
    } as DeleteDebtRequest;
    this.debtService.deleteDebt(newDebt)
      .subscribe(response => {
        this.router.navigate(['/history']);
      });
  }

}
