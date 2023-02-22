import { useEffect, useState } from "react";
import CompanyApi from "../api/CompanyApi";
import compareData from "../SortData";

const SortDirection = (direction, setSortDirection) => {
    return (
    <>
        <h5>{direction ? "ASCENDING" : "DESCENDING"}</h5>
            <button type="button" class="btn btn-secondary" onClick={()=>{setSortDirection(!direction)}}>Toggle</button>
        </>
    )
};

const CompanyData = () => {

    const [dataList, setDataList] = useState([]);
    const [sortAscending, setSortAscending]= useState(true);
    const [sortField, setSortField]= useState("companyName")

    useEffect(()=> {
        const fetchDataAsync = async () => {
            const data = await new CompanyApi().getAll();
            setDataList(data.sort((lhs, rhs) => compareData(lhs, rhs, sortField, sortAscending)));
        }
        
        fetchDataAsync();
    }, []);

    useEffect(() => {
        if (dataList.length > 0) {
            const ordered = dataList.sort((lhs, rhs) => compareData(lhs, rhs, sortField, sortAscending));
            setDataList(ordered);
        }
    }, [sortAscending, sortField]);

    if(dataList.length === 0) {
        return (<h3>Loading...</h3>);
    }

    return (
        <div>
        <h1 id="tabelLabel">Company data</h1>
            <div>
                <h6>Sort:</h6>
                <h5>{sortAscending ? "ASCENDING" : "DESCENDING"}</h5>
                <button type="button" class="btn btn-secondary" onClick={() => { setSortAscending(!sortAscending) }}>Toggle</button>
            </div>
        <table className='table table-striped' aria-labelledby="tabelLabel">
          <thead>
            <tr>
              <th onClick={()=>setSortField("companyName")}>Company Name</th>
              <th onClick={()=>setSortField("yearsInBusiness")}>Years In Business</th>
              <th onClick={()=>setSortField("contactName")}>Contact Name</th>
              <th onClick={()=>setSortField("contactPhone")}>Contact Phone</th>
              <th onClick={()=>setSortField("contactEmail")}>Contact Email</th>
            </tr>
          </thead>
          <tbody>
            {dataList.map(company =>
                <tr key={company.companyName}>
                    <td>{company.companyName}</td>
                    <td>{company.yearsInBusiness}</td>
                    <td>{company.contactName}</td>
                    <td>{company.contactPhone}</td>
                    <td>{company.contactEmail}</td>
              </tr>
            )}
          </tbody>
        </table>
        </div>
      );
};

export default CompanyData