import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DebtorService } from '../shared/services/debtor.service';
import { DebtorResponse } from '../shared/models/debtor-response.model';
import { Debtor } from '../shared/models/deptor';
import { Debt } from '../shared/models/debt';

@Component({
  selector: 'app-debtor-history',
  templateUrl: './debtor-history.component.html',
  styleUrls: ['./debtor-history.component.scss']
})
export class DebtorHistoryComponent implements OnInit {

  public debtorId: string;
  public debtor: Debtor;

  constructor(private route: ActivatedRoute,
    private debtorService: DebtorService) { }

  ngOnInit() {
    debugger
    this.route.params.subscribe(params => {
      this.debtorId = params['id'];
    });

    this.getDebtorHistory();
  }
  public getDebtorHistory(): void {
    this.debtorService.getGebtor(this.debtorId)
      .subscribe((response: DebtorResponse) => {
        this.debtor = {
          id: response.id,
          name: response.name,
          surname: response.surname,
          debts: response.debts.map(x => {
            return {
              id: x.id,
              amount: x.amount,
              creationDate: x.creationDate,
              description: x.description
            } as Debt;
          })
        } as Debtor;
      });
  }
}
