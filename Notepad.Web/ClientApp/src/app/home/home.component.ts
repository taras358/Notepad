import { Component, OnInit } from '@angular/core';
import { DebtorService } from '../shared/services/debtor.service';
import { DebtorResponse } from '../shared/models/debtor-response.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { DebtorRequest } from '../shared/models/debtor-request.model';
import { Debtor } from '../shared/models/deptor';
import { Router } from '@angular/router';
import { NavbarService } from '../shared/services/nav-bar.service';
import { Observable } from 'rxjs';
import { UpdateDebtRequest } from '../shared/models/update-debt-request';
import { UpdateDebtorRequest } from '../shared/models/update-debtor-request';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  public deptors: Array<Debtor>;
  public filtered: Array<Debtor>;
  public isAddDebtorFormActive: boolean;

  constructor(public debtorServise: DebtorService,
    private router: Router,
    private navbarService: NavbarService) { }

  public createDeptorFrom: FormGroup;
  public editDeptorFrom: FormGroup;
  public serachDeptorForm: FormGroup;

  ngOnInit() {
    this.deptors = [];
    this.filtered = [];
    this.isAddDebtorFormActive = false;
    this.getGebtors();

    this.createDeptorFrom = new FormGroup({
      name: new FormControl('', Validators.required),
      surname: new FormControl('', Validators.required)
    });
    this.editDeptorFrom = new FormGroup({
      name: new FormControl('', Validators.required),
      surname: new FormControl('', Validators.required)
    });
    this.serachDeptorForm = new FormGroup({
      fullName: new FormControl('', Validators.required)
    });
  }

  public onDebtorClick(debtor: Debtor) {
    if (debtor) {
      if (debtor.isEdited) {
        return;
      }
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
      this.isAddDebtorFormActive = false;
      this.serachDeptorForm.reset();
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
        this.filtered = response.debtors.map((x: DebtorResponse) => {
          return {
            id: x.id,
            name: x.name,
            surname: x.surname,
            totalDebt: x.totalDebt,
            isEdited: false
          } as Debtor;
        });
        this.initFormGroup(this.filtered);
      });
  }
  private initFormGroup(debtors: Debtor[]) {
    if (debtors) {
      this.editDeptorFrom = new FormGroup({});
      debtors.forEach(debtor => {
        this.editDeptorFrom.addControl(`name${debtor.id}`, new FormControl('', Validators.required))
        this.editDeptorFrom.addControl(`surname${debtor.id}`, new FormControl('', Validators.required))
      });
    }
  }

  private findDebtors(query: string): void {
    if (query) {
      this.debtorServise.findDebtors(query)
        .subscribe(response => {
          if (response && response.debtors.length > 0) {
            this.filtered = response.debtors.map((x: DebtorResponse) => {
              return {
                id: x.id,
                name: x.name,
                surname: x.surname,
                totalDebt: x.totalDebt
              } as Debtor;
            });
            this.isAddDebtorFormActive = false;
          } else {
            this.isAddDebtorFormActive = true;
            this.serachDeptorForm.reset();
            this.initAddDebtorForm(query);
          }
        });
    }
  }
  private initAddDebtorForm(query: string) {
    if (query) {
      const debtorFullName = query.split(' ');
      if (debtorFullName) {
        this.createDeptorFrom.controls.name.setValue(debtorFullName[0]);
        this.createDeptorFrom.controls.surname.setValue(debtorFullName[1]);
      }
    }
  }
  private initEditDebtorForm(debtor: Debtor) {
    if (debtor) {
      this.editDeptorFrom.get(`name${debtor.id}`).setValue(debtor.name);
      this.editDeptorFrom.get(`surname${debtor.id}`).setValue(debtor.surname);
    }
  }

  public onEditDebtorClick(debtor: Debtor) {
    debugger
    event.stopPropagation();
    if (debtor.isEdited) {
      const newName = this.editDeptorFrom.get(`name${debtor.id}`).value;
      const newSurname = this.editDeptorFrom.get(`surname${debtor.id}`).value;
      const updatedDebtor = {
        id: debtor.id,
        name: newName,
        surname: newSurname
      } as UpdateDebtorRequest;
      this.debtorServise.updateDebtor(updatedDebtor)
        .subscribe(response => {
          debtor.name = newName;
          debtor.surname = newSurname;
          debtor.isEdited = !debtor.isEdited;
        });
    } else {
      this.initEditDebtorForm(debtor);
      debtor.isEdited = !debtor.isEdited;
    }
  }
  public onDeleteDebtorClick(debtor: Debtor, event) {
    event.stopPropagation();
    if (debtor) {
      this.debtorServise.deleteDebtor(debtor.id)
        .subscribe(() => {
          const index = this.filtered.findIndex(x => x.id === debtor.id);
          this.filtered.splice(index, 1);
          this.navbarService.removeDebtor();
        });
    }
  }
}


