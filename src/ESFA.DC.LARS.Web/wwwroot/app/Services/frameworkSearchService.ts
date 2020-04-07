import axios from 'axios'
import qs from 'qs';
import { IFilterItem, FilterType } from '../Interfaces/IFilterItem';
import { IFrameworksSearchRequest } from '../Interfaces/IFrameworksSearchRequest';
import { ISearchResults } from '../Interfaces/ISearchResults';
import { filterService } from '../Services/filterService';

class FrameworkSearchService {

	async getResultsAsync(filters: Array<IFilterItem>, searchTerm: string): Promise<ISearchResults>{
		const response = await axios.get<ISearchResults>('../FrameworkSearchResult/Results', {
			params: this.createRequest(filters, searchTerm), paramsSerializer: p =>  qs.stringify(p)
		});
		return response.data;
	}

	private createRequest(filters: Array<IFilterItem>, searchTerm: string): IFrameworksSearchRequest {
		const request: IFrameworksSearchRequest = {
			searchTerm: searchTerm,
			frameworkTypes: filterService.filterValuesForType(filters, FilterType.FrameworkTypes),
			issuingAuthorities: filterService.filterValuesForType(filters, FilterType.IssuingAuthorities)
		};

		return request;
	}
}

export const frameworkSearchService = new FrameworkSearchService();