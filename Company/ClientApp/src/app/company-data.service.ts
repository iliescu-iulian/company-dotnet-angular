

import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';
import { Observable, Subscription, throwError, BehaviorSubject, combineLatest, EMPTY, Subject } from 'rxjs';
import { catchError, tap, map } from 'rxjs/operators';
import { saveAs } from "file-saver";
import { CompanyDataSort } from "./company-data-sort";
import { CompanyDataFields } from "./company-data-fields";

@Injectable({
  providedIn: 'root'
})
export class CompanyDataService {

  private companyData$: Observable<ICompanyData[]>;
  private sortBySubject = new BehaviorSubject<string>('');
  sortByAction$ = this.sortBySubject.asObservable();
  sortedCompanyData$: Observable<ICompanyData[]>;
  private _sortFielddisplayName: string = '';

  get sortFieldDisplayName(): string {
    return this._sortFielddisplayName;
  }

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getAll(): Observable<ICompanyData[]> {
    console.log('service-getAll');
    if (this._sortFielddisplayName === '') {
      this.sortBy(CompanyDataFields.companyName);
      console.log('service-getAll-set sort field');
    }
    this.companyData$= this.httpClient.get<ICompanyData[]>(this.baseUrl + 'api/company').pipe(
      tap(data => console.log('retrieved: ' + JSON.stringify(data))));
    this.sortedCompanyData$ = combineLatest([this.companyData$, this.sortByAction$])
      .pipe(
        map(([companyData, sortField]) => {
            console.log('service-getAll into map');
            this._sortFielddisplayName = sortField;
            const sort = new CompanyDataSort(sortField);
            return sort.sort(companyData);
          }
        ));
    return this.sortedCompanyData$;
  }

  sortBy(fieldName: CompanyDataFields) {
    this.sortBySubject.next(fieldName);
  }

  private toCsv(data: ICompanyData[]) {
    const replacer = (key, value) => value === null ? '' : value;
    let csv = data.map(row =>
      Object.values(row).map(val => JSON.stringify(val, replacer)).join(','));
    return csv.join('\r\n');
  }

  downloadAsCsv(): void {
    this.sortedCompanyData$.subscribe((data) => {
      const blob: Blob = new Blob([this.toCsv(data)],
        {
          type: "text/csv;charset=utf-8"
        });
      saveAs(blob, "companyData.csv");
    });
  }

  private handleError(err: HttpErrorResponse) {
    let errorMessage = '';
    if (err.error instanceof ErrorEvent) {
      // client side or network error
      errorMessage = `An error occured: ${err.error.message}`;
    } else {
      // backend returned an error response code
      errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
    }

    console.error(errorMessage);
    return throwError(errorMessage);
  }
}
