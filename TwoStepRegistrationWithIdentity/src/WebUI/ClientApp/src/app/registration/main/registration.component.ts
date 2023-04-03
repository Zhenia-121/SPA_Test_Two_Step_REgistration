import { CdkStep, CdkStepper } from '@angular/cdk/stepper';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, take } from 'rxjs';
import { RegisterRequest } from 'src/models/register-request';
import { AccountsService } from 'src/services/accounts.service';

@Component({
  selector: 'registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  registerRequest$ = new BehaviorSubject<RegisterRequest>(<RegisterRequest> {
    email: '',
    password: '',
    confirmedPassword: '',
    isAgreementAccepted: false
  });

  firstStepFormGroup = this._formBuilder.group({
    firstCtrl: [false, Validators.requiredTrue],
  });

  secondStepFormGroup = this._formBuilder.group({
    secondCtrl: ['', Validators.required],
  });

  isLoading = false;
  successRegistration = false;

  @ViewChild(CdkStepper)
  cdkStepper!: CdkStepper;

  constructor(
    private _formBuilder: FormBuilder,
    private accountsService: AccountsService) {}

  ngOnInit() {
  }

  onFirstStepCompleted(stepFields: Partial<RegisterRequest>) {
    this.handleStepFieldsUpdate(stepFields);

    this.firstStepFormGroup?.get('firstCtrl')?.setValue(true);

    this.cdkStepper.next();
  }

  onSecondStepCompleted(stepFields: Partial<RegisterRequest>) {
    this.handleStepFieldsUpdate(stepFields);
    this.makeRegisterRequest();
  }

  private makeRegisterRequest() {
    this.isLoading = true;
    this.accountsService.register(this.registerRequest$.value)
      .pipe(
        take(1))
      .subscribe(() => {
        this.isLoading = false;
        this.successRegistration = true;
      });
  }

  private handleStepFieldsUpdate(stepFields: Partial<RegisterRequest>) {
    var request = { ...this.registerRequest$.value };

    Object.assign(request, stepFields);

    this.registerRequest$.next(request);
  }
}
