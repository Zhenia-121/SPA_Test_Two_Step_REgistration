import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';
import { ApiErrorResponse } from 'src/models/api-response-error';

@Injectable()
export abstract class ApiBaseService {

  protected handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      console.error('An error occurred:', error.error);
      return throwError(() => new Error('Connection issue occured.'));
    } else {
      const apiError = Object.assign({} as ApiErrorResponse, error.error);
      console.error(`Backend returned code ${error.status}, body was: `, error.error);
      return throwError(() => apiError);
    }
  }
}
