import { Component, OnInit } from '@angular/core';
import { Observable, EMPTY, Subject } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { CompanyDataService } from "../company-data.service";
import { CompanyDataFields } from "../company-data-fields";

@Component({
  selector: 'app-company-data',
  templateUrl: './company-data.component.html'
})
export class CompanyDataComponent implements OnInit {

  private errorMessageSubject = new Subject<string>();
  errorMessage$ = this.errorMessageSubject.asObservable();
  companyData$: Observable<ICompanyData[]>;
  fieldHeaders: string[];

  get sortField(): string {
    return this.companyDataService.sortFieldDisplayName;
}

  ngOnInit(): void {
    this.fieldHeaders = Array.from(Object.values(CompanyDataFields));
    this.companyData$ = this.companyDataService.getAll().pipe(
      catchError(err => {
        this.errorMessageSubject.next(err);
        return EMPTY;
      }));
  }

  onSort(sortField: CompanyDataFields): void {
    this.companyDataService.sortBy(sortField);
  }

  constructor(private companyDataService: CompanyDataService) {
    
  }
}


