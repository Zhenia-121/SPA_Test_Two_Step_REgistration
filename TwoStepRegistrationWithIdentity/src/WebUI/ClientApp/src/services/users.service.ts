import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable, catchError, debounceTime, delay, of } from 'rxjs';
import { UserRegisterRequest } from 'src/models/register-request';
import { ApiBaseService } from './api-base.service';
import { AppConfigService } from 'src/config/app-config.service';

@Injectable({
  providedIn: 'root'
})
export class UsersService extends ApiBaseService {
baseRoute: string = 'Users';

constructor(private http: HttpClient, private config: AppConfigService) {
  super();
}

public register(registerRequest: UserRegisterRequest): Observable<void> {
  return this.http.post<void>(`${this.config.apiUrl}/${this.baseRoute}/Register`, registerRequest).pipe(
    catchError((error) => this.handleError(error))
  );
}

  public isLoginAlreadyTaken(login: string): Observable<boolean> {
    return of(false).pipe(
      delay(100)
    );
  }
}
