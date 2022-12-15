import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { CompanyDataService } from "../company-data.service";
import { CompanyDataFields } from "../company-data-fields";

@Component({
  selector: 'app-company-data',
  templateUrl: './company-data.component.html'
})
export class CompanyDataComponent implements OnInit {

  private companyData$: Observable<ICompanyData[]>;
  private fieldHeaders: string[];

  get sortField(): string {
    return this.companyDataService.sortFieldDisplayName;
}

  ngOnInit(): void {
    this.fieldHeaders = Array.from(Object.values(CompanyDataFields));
    this.companyData$ = this.companyDataService.getAll();
  }

  onSort(sortField: CompanyDataFields): void {
    this.companyDataService.sortBy(sortField);
  }

  constructor(private companyDataService: CompanyDataService) {
    
  }
}


