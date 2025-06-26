import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { TuiButton } from '@taiga-ui/core';

import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-store-activate',
  templateUrl: './store-activate.html',
  styleUrl: './store-activate.less',
  imports: [ReactiveFormsModule, CommonModule, TuiButton]
})
export class StoreActivate {
  step = 0;

  storeForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.storeForm = this.fb.group({
      storeName: [''],
      storeAddress: [''],
      storePhone: [''],
      ownerName: ['',],
      ownerEmail: ['', ],
      ownerPhone: ['', ],
    });
  }

  nextStep() {
    console.log('nextStep called — step:', this.step, 'form valid:', this.storeForm.valid);
    if (this.step === 0) {
      this.step = 1;
    } else if (this.step === 1 && this.storeForm.valid) {
      this.step = 2;
    } else if (this.step === 1) {
      this.storeForm.markAllAsTouched();
    }
  }

  prevStep() {
    if (this.step > 0) {
      this.step--;
    }
  }

  onSubmit() {
    if (this.storeForm.valid) {
      console.log('Форма отправлена:', this.storeForm.value);
      // Здесь можно отправить данные на сервер
    }
  }
}