import { Inject, Injectable } from '@angular/core';
import { ApiBaseService } from './api-base.service';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, delay, of } from 'rxjs';
import { Country, Province } from 'src/models/country';
import { AppConfigService } from 'src/config/app-config.service';

@Injectable()
export class CountriesService extends ApiBaseService {
  baseRoute: string = 'Countries';

  constructor(private http: HttpClient, private config: AppConfigService) {
    super();
  }

  public getAllCountries(): Observable<Country[]> {
    return this.http.get<Country[]>(`${this.config.apiUrl}/${this.baseRoute}`).pipe(
      catchError((error) => this.handleError(error))
    );
  }

  public getCountryProvinces(countryId: number): Observable<Province[]> {
    return this.http.get<Province[]>(`${this.config.apiUrl}/${this.baseRoute}/${countryId}/Provinces`).pipe(
      catchError((error) => this.handleError(error))
    );
  }
}
