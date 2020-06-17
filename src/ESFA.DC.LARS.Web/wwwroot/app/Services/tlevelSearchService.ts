import axios from 'axios'
import qs from 'qs';
import { IFilterItem } from '../Interfaces/IFilterItem';
import { ISearchResults } from '../Interfaces/ISearchResults';
import { ITLevelSearchRequest } from '../Interfaces/ITLevelSearchRequest';

class TLevelSearchService {

	async getResultsAsync(filters: Array<IFilterItem>, searchTerm: string): Promise<ISearchResults>{
		const response = await axios.get<ISearchResults>('../TLevelSearchResult/Results', {
			params: this.createRequest(filters, searchTerm), paramsSerializer: p =>  qs.stringify(p)
		});
		return response.data;
	}

	private createRequest(filters: Array<IFilterItem>, searchTerm: string): ITLevelSearchRequest {
		const request: ITLevelSearchRequest = {
			searchTerm: searchTerm,
		};

		return request;
	}
}

export const tlevelSearchService = new TLevelSearchService();