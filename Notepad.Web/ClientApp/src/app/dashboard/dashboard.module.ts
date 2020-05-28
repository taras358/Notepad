import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardComponent } from './dashboard.component';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { HomeComponent } from './home/home.component';
import { MatIconModule } from '@angular/material/icon';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatMenuModule } from '@angular/material/menu';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';

@NgModule({
  imports: [
    CommonModule,
    MatIconModule,
    FormsModule,
    ReactiveFormsModule,
    MatMenuModule,
    MatDialogModule,
    MatDatepickerModule,
    MatNativeDateModule,
    DashboardRoutingModule
  ],
  declarations: [
    DashboardComponent,
    HomeComponent
  ]
})
export class DashboardModule { }
