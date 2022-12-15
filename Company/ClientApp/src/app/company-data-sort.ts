import { CompanyDataFields } from "./company-data-fields";

export class CompanyDataSort {
  fieldMapper: Map<string, string>;
  private sortField: string;

  constructor(sortFieldDisplayName: string) {
    this.fieldMapper = new Map<string, string>();
    Object.keys(CompanyDataFields).forEach(key =>
      this.fieldMapper.set(CompanyDataFields[key], key));
    this.sortField = this.fieldMapper.get(sortFieldDisplayName);
  }

  sort(companyData: ICompanyData[]) {
    return companyData.sort((lhs, rhs) => {
      const result = this.genericSortFieldHandler(lhs[this.sortField], rhs[this.sortField]);
      if (result === 0 && this.sortField === CompanyDataFields.yearsInBusiness) {
        const fieldName = CompanyDataFields.companyName;
        return this.genericSortFieldHandler(lhs[fieldName], rhs[fieldName]);
      }
      return result;
    });
  }

  private genericSortFieldHandler(lhs, rhs) {
    if (lhs === rhs) {
      return 0;
    }
    if (lhs === null) {
      return 1;
    }
    if (rhs === null) {
      return -1;
    }
    return lhs > rhs ? 1 : -1;
  }
}
