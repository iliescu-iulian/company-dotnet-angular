import axios from 'axios';

class CompanyApi {
    async getAll() {
        const result = await axios.get('api/company');
        const data = (result?.status === 200) ? result.data : {};
        return data;
    }
}

export default CompanyApi;