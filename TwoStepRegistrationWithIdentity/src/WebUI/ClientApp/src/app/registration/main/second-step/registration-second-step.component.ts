import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, UntypedFormControl, Validators } from '@angular/forms';
import { MatSelectChange } from '@angular/material/select';
import { BehaviorSubject, Observable, filter, switchMap, tap } from 'rxjs';
import { Province } from 'src/models/country';
import { Country } from 'src/models/country';
import { UserRegisterRequest } from 'src/models/register-request';
import { CountriesService } from 'src/services/countries.service';

@Component({
  selector: 'registration-second-step',
  templateUrl: './registration-second-step.component.html',
  styleUrls: ['./registration-second-step.component.scss'],
  providers: [CountriesService]
})
export class RegistrationSecondStepComponent implements OnInit {
  @Output() stepCompleted = new EventEmitter<Partial<UserRegisterRequest>>();

  countries$!: Observable<Country[]>;
  provinces$!: Observable<Province[]>;

  selectContryControl = new FormControl<number | null>(null, Validators.required);
  selectProvinceControl = new FormControl<number | null>({value: null, disabled: true }, Validators.required);
  isProvincesLoading = false;

  constructor(private countriesService: CountriesService) {
  }

  ngOnInit() {
    this.countries$ = this.countriesService.getAllCountries();
    this.provinces$ = this.selectContryControl.valueChanges
    .pipe(
      filter((countryId): countryId is number => !!countryId),
      tap(() => {
        this.isProvincesLoading = true;
        if (this.selectProvinceControl.disabled) {
          this.selectProvinceControl.enable();
        }
        this.selectProvinceControl.setValue(null);
      }),
      switchMap((countryId) => this.countriesService.getCountryProvinces(countryId).pipe((tap(() => this.isProvincesLoading = false)))));
  }

  onSubmit(): void {
    if (this.selectProvinceControl.value == null) {
      return;
    }

    this.stepCompleted.emit({ provinceId: +this.selectProvinceControl.value});
  }
}
