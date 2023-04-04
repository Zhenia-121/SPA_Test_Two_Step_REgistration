import { CdkStepper } from '@angular/cdk/stepper';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { BehaviorSubject, Observable, finalize, take } from 'rxjs';
import { ApiErrorResponse } from 'src/models/api-response-error';
import { UserRegisterRequest } from 'src/models/register-request';
import { UsersService } from 'src/services/users.service';

@Component({
  selector: 'registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  registerRequest$ = new BehaviorSubject<UserRegisterRequest>(<UserRegisterRequest> {
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
  showRegistration = true;
  errors = new BehaviorSubject<string>('');

  @ViewChild(CdkStepper)
  cdkStepper!: CdkStepper;

  constructor(
    private _formBuilder: FormBuilder,
    private usersService: UsersService) {}

  ngOnInit() {
  }

  onFirstStepCompleted(stepFields: Partial<UserRegisterRequest>) {
    this.handleStepFieldsUpdate(stepFields);

    this.firstStepFormGroup?.get('firstCtrl')?.setValue(true);

    this.cdkStepper.next();
  }

  onSecondStepCompleted(stepFields: Partial<UserRegisterRequest>) {
    this.handleStepFieldsUpdate(stepFields);
    this.makeRegisterRequest();
  }

  public onShowRegistrationClicked(): void {
    this.showRegistration = true;
  }

  private makeRegisterRequest() {
    this.isLoading = true;
    this.showRegistration = false;
    this.usersService.register(this.registerRequest$.value)
      .pipe(
        take(1),
        finalize(() => this.isLoading = false))
      .subscribe({
        next: () => this.successRegistration = true,
        error: (error: ApiErrorResponse) => {
          this.successRegistration = false;
          this.errors.next(error?.description || 'Default error');
        }
      });
  }

  private handleStepFieldsUpdate(stepFields: Partial<UserRegisterRequest>) {
    var request = { ...this.registerRequest$.value };

    Object.assign(request, stepFields);

    this.registerRequest$.next(request);
  }
}
