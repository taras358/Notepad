import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { CommonModule } from '@angular/common';
import { CanActivateDebtor } from './shared/guards/can-activete-debtor.guard';
import { AuthGuard } from './shared/guards/auth.guard';

@NgModule({
   declarations: [
      AppComponent
   ],
   imports: [
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      AppRoutingModule,
      CommonModule,
      BrowserModule,
   ],
   entryComponents: [
   ],
   providers: [
      CanActivateDebtor,
      AuthGuard
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
