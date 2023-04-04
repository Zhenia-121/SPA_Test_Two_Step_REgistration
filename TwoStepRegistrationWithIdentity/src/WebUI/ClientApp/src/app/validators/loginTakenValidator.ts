import { Injectable } from "@angular/core";
import { AbstractControl, AsyncValidator, ValidationErrors } from "@angular/forms";
import { Observable, catchError, map, of } from "rxjs";
import { UsersService } from "src/services/users.service";

@Injectable({ providedIn: 'root' })
export class UniqueLoginValidator implements AsyncValidator {
  constructor(private accountsService: UsersService) {}

  validate(
    control: AbstractControl
  ): Observable<ValidationErrors | null> {
    return this.accountsService.isLoginAlreadyTaken(control.value).pipe(
      map(isTaken => (isTaken ? { uniqueLogin: true } : null)),
      catchError(() => of(null))
    );
  }
}
