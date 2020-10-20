import axios from 'axios'
import qs from 'qs';
import { IFilterItem } from '../Interfaces/IFilterItem';
import { FilterType } from '../Enums/FilterType';
import { IStandardSearchRequest } from '../Interfaces/IStandardSearchRequest';
import { ISearchResults } from '../Interfaces/ISearchResults';
import { filterService } from './filterService';

class StandardSearchService {

	async getResultsAsync(filters: Array<IFilterItem>, searchTerm: string): Promise<ISearchResults>{
		const response = await axios.get<ISearchResults>('../StandardsSearchResult/Results', {
			params: this.createRequest(filters, searchTerm), paramsSerializer: p =>  qs.stringify(p)
		});
		return response.data;
	}

	private createRequest(filters: Array<IFilterItem>, searchTerm: string): IStandardSearchRequest {
		const request: IStandardSearchRequest = {
			searchTerm: searchTerm,
			levels: filterService.filterValuesForType(filters, FilterType.Levels),
			sectors: filterService.filterValuesForType(filters, FilterType.Sectors)
		};

		return request;
	}
}

export const standardSearchService = new StandardSearchService();