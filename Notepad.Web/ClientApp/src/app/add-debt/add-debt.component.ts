import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CreateDebtRequest } from '../shared/models/create-debt-request';
import { DebtService } from '../shared/services/debt.service';

@Component({
  selector: 'app-add-debt',
  templateUrl: './add-debt.component.html',
  styleUrls: ['./add-debt.component.scss']
})
export class AddDebtComponent implements OnInit {

  public addDebtGroup: FormGroup;
  public debtorId: string;

  constructor(private route: ActivatedRoute,
    private debtService: DebtService,
    private router: Router) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.debtorId = params['id'];
    });

    this.addDebtGroup = new FormGroup({
      amount: new FormControl(0, Validators.required)
    });
  }


  public onAddDebtClick() {
    const amount: number = parseFloat(this.addDebtGroup.controls.amount.value);
    const newDebt = {
      debtorId: this.debtorId,
      amount: amount,
      description: 'Test'
    } as CreateDebtRequest;
    this.debtService.createDebt(newDebt)
      .subscribe(response => {
        this.router.navigate(['/history', this.debtorId]);
      });

  }

}
