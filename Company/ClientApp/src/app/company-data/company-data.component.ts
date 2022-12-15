import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { CompanyDataService } from "../company-data.service";
import { CompanyDataFields } from "../company-data-fields";

@Component({
  selector: 'app-company-data',
  templateUrl: './company-data.component.html'
})
export class CompanyDataComponent implements OnInit, OnDestroy {

  private companyData$: Observable<ICompanyData[]>;
  //private sortedData$: Observable<ICompanyData[]>;
    // private sortField: string;
  /*private fields = new Map<string, string>([
    ["Company Name", "companyName"],
    ["Years in business", "yearsInBusiness"],
    ["Contact Name", "contactName"],
    ["Contact Email", "contactEmail"],
    ["Contact Phone", "contactPhone"]
  ]);*/
  private fieldHeaders: string[];

  get sortField(): string {
    return this.companyDataService.sortFieldDisplayName;
}

  ngOnInit(): void {
    // this.sortField = "Company Name";
    this.fieldHeaders = Array.from(Object.values(CompanyDataFields));
    this.companyData$ = this.companyDataService.getAll();
    // this.sort();
  }

  ngOnDestroy(): void { throw new Error("Not implemented"); }

  /*private sort(): void {
    const fieldName = this.fields.get(this.sortField);
    console.log(`sorting ${this.sortField} using field ${fieldName}`);
    this.sortedData$ = this.companyData$.pipe(
      tap(r => {
        r.sort((a, b) => {
          
          const ret = (a[fieldName] > b[fieldName]) ? 1 : -1;
          console.log(`${a[fieldName]} vs ${b[fieldName]} = ${ret}`);
          return ret;
        });
        return r;
      }));
  }*/

  onSort(sortField: CompanyDataFields): void {
    this.companyDataService.sortBy(sortField);
    // this.sortField = sortField;
    // this.sort();
  }

  constructor(private companyDataService: CompanyDataService) {
    
  }
}


