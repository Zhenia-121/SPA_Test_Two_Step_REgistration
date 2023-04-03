import { Injectable } from "@angular/core";
import { AbstractControl, AsyncValidator, ValidationErrors } from "@angular/forms";
import { Observable, catchError, map, of } from "rxjs";
import { AccountsService } from "src/services/accounts.service";

@Injectable({ providedIn: 'root' })
export class UniqueLoginValidator implements AsyncValidator {
  constructor(private accountsService: AccountsService) {}

  validate(
    control: AbstractControl
  ): Observable<ValidationErrors | null> {
    return this.accountsService.isLoginAlreadyTaken(control.value).pipe(
      map(isTaken => (isTaken ? { uniqueLogin: true } : null)),
      catchError(() => of(null))
    );
  }
}
