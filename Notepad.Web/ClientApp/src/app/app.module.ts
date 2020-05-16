import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { AddDebtComponent } from './add-debt/add-debt.component';
import { AppRoutingModule  } from './app-routing.module';
import { DebtorHistoryComponent } from './debtor-history/debtor-history.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { DeleteDebtComponent } from './delete-debt/delete-debt.component';

@NgModule({
   declarations: [
      AppComponent,
      HomeComponent,
      AddDebtComponent,
      DebtorHistoryComponent,
      NavBarComponent,
      DeleteDebtComponent
   ],
   imports: [
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      AppRoutingModule,
      BrowserModule
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
