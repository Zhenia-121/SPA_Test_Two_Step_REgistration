import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable, catchError, debounceTime, delay, of } from 'rxjs';
import { RegisterRequest } from 'src/models/register-request';
import { ApiBaseService } from './api-base.service';

@Injectable({
  providedIn: 'root'
})
export class AccountsService extends ApiBaseService {
baseRoute: string = 'accounts';

constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  super();
}

public register(registerRequest: RegisterRequest): Observable<void> {
  return of(void 0).pipe(delay(200));

  // return this.http.post<void>(`${this.baseUrl}/${this.baseRoute}`, registerRequest).pipe(
  //   catchError((error) => this.handleError(error))
  // );
}

  public isLoginAlreadyTaken(login: string): Observable<boolean> {
    return of(false).pipe(
      delay(100)
    );
  }
}
