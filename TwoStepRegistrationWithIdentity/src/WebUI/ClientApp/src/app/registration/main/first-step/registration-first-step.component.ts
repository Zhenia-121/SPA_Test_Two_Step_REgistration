import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { matchValidator } from 'src/app/validators/confirmPasswordValidator';
import { RegisterRequest } from 'src/models/register-request';

@Component({
  selector: 'registration-first-step',
  templateUrl: './registration-first-step.component.html',
  styleUrls: ['./registration-first-step.component.scss']
})
export class RegistrationFirstStepComponent implements OnInit {
  @Output() stepCompleted = new EventEmitter<Partial<RegisterRequest>>();

  passwordRegex = /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{2,}$/gm;
  hidePassword = true;
  submitted = false;

  formGroup: FormGroup = this._formBuilder.group({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.pattern(this.passwordRegex), matchValidator('confirmPassword', true)]),
    confirmPassword: new FormControl('', [Validators.required, Validators.pattern(this.passwordRegex), matchValidator('password')]),
    agreementAccepted: new FormControl(false, [Validators.requiredTrue])
  });

  constructor(private _formBuilder: FormBuilder) { }

  ngOnInit() {
  }

  get f() { return this.formGroup.controls; }

  onSubmit() {
    this.submitted = true;

    if (this.formGroup.invalid) {
      return;
    }

    this.stepCompleted.emit({
      email: this.formGroup.get('email')?.value,
      password: this.formGroup.get('password')?.value,
      confirmedPassword: this.formGroup.get('confirmPassword')?.value,
      isAgreementAccepted: this.formGroup.get('agreementAccepted')?.value
    });
  }
}
