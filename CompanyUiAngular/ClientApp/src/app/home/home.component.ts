import { Component } from '@angular/core';
import { CompanyDataService } from "../company-data.service";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  constructor(private companyDataService: CompanyDataService) { }

  get sortField(): string {
    return this.companyDataService.sortFieldDisplayName;
  }

  downloadCsv(): void {
    this.companyDataService.downloadAsCsv();
  }
}
