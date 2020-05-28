import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { DebtorService } from '../shared/services/debtor.service';
import { Debtor } from '../shared/models/deptor';
import { NavbarService } from '../shared/services/nav-bar.service';
import * as moment from 'moment';
import { saveAs } from 'file-saver';
import { DownloadReportModel } from '../shared/models/download-report.model';
@Component({
  selector: 'app-download-report',
  templateUrl: './download-report.component.html',
  styleUrls: ['./download-report.component.scss']
})
export class DownloadReportComponent implements OnInit {

  public debtor: Debtor;
  public currentDay: Date;
  public minDate: Date;

  public datePickerForm: FormGroup;
  constructor(private debtorService: DebtorService,
    private navbarService: NavbarService) { }

  ngOnInit() {
    this.debtor = this.debtorService.getCurrentDebtor();
    this.navbarService.updateLinks();
    this.datePickerForm = new FormGroup({
      beginDate: new FormControl('', Validators.required),
      endDate: new FormControl('', Validators.required)
    });
    this.currentDay = moment().toDate();
  }
  public onDownloadExcelBtnClick() {
    if (this.datePickerForm.valid) {
      const beginDate = moment(this.datePickerForm.controls.beginDate.value).format('MM/DD/YYYY');
      const endDate = moment(this.datePickerForm.controls.endDate.value).format('MM/DD/YYYY');
      const downloadReportModel = {
        debtorId: this.debtor.id,
        beginDate: beginDate.toString(),
        endDate: endDate.toString()
      } as DownloadReportModel;
      this.debtorService.downloadExcelReport(downloadReportModel)
        .subscribe(response => {
          const fileName = `Report_${this.debtor.name}_${this.debtor.surname}.xlsx`;
          saveAs(response, fileName);
        });
    }
  }
  public onDownloadPdfBtnClick() {
    if (this.datePickerForm.valid) {
      const beginDate = moment(this.datePickerForm.controls.beginDate.value).format('MM/DD/YYYY');
      const endDate = moment(this.datePickerForm.controls.endDate.value).format('MM/DD/YYYY');
      const downloadReportModel = {
        debtorId: this.debtor.id,
        beginDate: beginDate.toString(),
        endDate: endDate.toString()
      } as DownloadReportModel;
      this.debtorService.downloadPdfReport(downloadReportModel)
        .subscribe(response => {
          const fileName = `Report_${this.debtor.name}_${this.debtor.surname}.pdf`;
          saveAs(response, fileName);
        });
    }
  }
  public onDateChangeClick(event: any) {
  }
}
