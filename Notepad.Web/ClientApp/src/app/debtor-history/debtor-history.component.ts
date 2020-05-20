import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DebtorService } from '../shared/services/debtor.service';
import { DebtorResponse } from '../shared/models/debtor-response.model';
import { Debtor } from '../shared/models/deptor';
import { Debt } from '../shared/models/debt';
import { NavbarService } from '../shared/services/nav-bar.service';
import { UpdateDebtRequest } from '../shared/models/update-debt-request';
import { DebtService } from '../shared/services/debt.service';

@Component({
  selector: 'app-debtor-history',
  templateUrl: './debtor-history.component.html',
  styleUrls: ['./debtor-history.component.scss']
})
export class DebtorHistoryComponent implements OnInit {

  public debtor: Debtor;

  constructor(private route: ActivatedRoute,
    private debtorService: DebtorService,
    private debtService: DebtService,
    private navbarService: NavbarService) { }

  ngOnInit() {
    debugger
    this.debtor = this.debtorService.getCurrentDebtor();
    if (this.debtor) {
      this.getDebtorHistory();
    }
    this.navbarService.updateLinks();
  }

  public onDebtChancheClick(debt: Debt) {
    if (debt) {
      const updateDebtRequest = {
        id: debt.id,
        creationDate: debt.creationDate,
        debtorId: this.debtor.id,
        description: debt.description,
        amount: debt.amount,
        isRepaid: !debt.isRepaid
      } as UpdateDebtRequest;
      this.debtService.updateDebt(updateDebtRequest)
        .subscribe(response => {
          debt.isRepaid = !debt.isRepaid;
          this.getDebtorHistory();
        });
    }
  }

  public getDebtorHistory(): void {
    this.debtorService.getGebtor(this.debtor.id)
      .subscribe((response: DebtorResponse) => {
        if (response) {
          this.debtor = {
            id: response.id,
            name: response.name,
            surname: response.surname,
            debts: response.debts.map(x => {
              return {
                id: x.id,
                amount: x.amount,
                creationDate: x.creationDate,
                description: x.description,
                isRepaid: x.isRepaid
              } as Debt;
            })
          } as Debtor;
        }
      });
  }
}
