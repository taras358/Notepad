import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-are-you-sure-dialog',
  templateUrl: './are-you-sure-dialog.component.html',
  styleUrls: ['./are-you-sure-dialog.component.scss']
})
export class AreYouSureDialogComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<AreYouSureDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
  }

  public onYesBtnClick(): void {
    this.dialogRef.close(true);
  }
  public onNoClick(): void {
    this.dialogRef.close(false);
  }
}
