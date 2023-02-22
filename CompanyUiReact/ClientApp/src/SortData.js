function genericCompare(lhs, rhs, ascending) {
    if (lhs === rhs) {
        return 0;
      }
      if (!lhs) {
        return 1;
      }
      if (!rhs) {
        return -1;
      }

      return lhs > rhs ? 1 : -1;
}

export default function compareData(lhs, rhs, sortField, ascending) {
    const unit = ascending ? 1 : -1;
    const result = genericCompare(lhs[sortField], rhs[sortField], ascending);
    if(result === 0 && sortField === "yearsInBusiness") {
        // sort also by company name
        return genericCompare(lhs["companyName"], rhs["companyName"], ascending) * unit;
    }
    return result * unit;
}
