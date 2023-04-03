import { Inject, Injectable } from '@angular/core';
import { ApiBaseService } from './api-base.service';
import { HttpClient } from '@angular/common/http';
import { Observable, delay, of } from 'rxjs';
import { Country, Province } from 'src/models/country';

@Injectable()
export class CountriesService extends ApiBaseService {
  baseRoute: string = 'countries';

  private fakeCountries: Country[] = [
    { name: 'Unitied States', id: 1, code: 'USA' },
    { name: 'Unitied Kingdom', id: 2, code: 'UK' },
  ]

  private fakeProvinces: Province[] = [
    { name: 'USA Province 1', id: 1, countryId: 1 },
    { name: 'USA Province 2', id: 2, countryId: 1 },
    { name: 'UK Province 1', id: 3, countryId: 2 },
    { name: 'UK Province 2', id: 4, countryId: 2 },
  ]

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    super();
  }

  public getAllCountries(): Observable<Country[]> {

    return of(this.fakeCountries).pipe(delay(100));

    // return this.http.get<Country[]>(`${this.baseUrl}/${this.baseRoute}`).pipe(
    //   catchError((error) => this.handleError(error))
    // );
  }

  public getCountryProvinces(countryId: number): Observable<Province[]> {

    return of(this.fakeProvinces.filter(p => p.countryId === countryId)).pipe(delay(100));

    // return this.http.get<Country[]>(`${this.baseUrl}/${this.baseRoute}/${countryId}/provinces/`).pipe(
    //   catchError((error) => this.handleError(error))
    // );
  }
}
