import axios from 'axios'
import { IFilterItem, FilterType } from '../Interfaces/IFilterItem';
import qs from 'qs';
import { ISearchRequest } from '../Interfaces/ISearchRequest';
import { ISearchResults } from '../Interfaces/ISearchResults';

class SearchService {

	async getResultsAsync(filters: Array<IFilterItem>): Promise<ISearchResults>{
		var response = await axios.get<ISearchResults>('../LearningAimSearchResult/Results', {
			params: this.createRequest(filters), paramsSerializer: p =>  qs.stringify(p)
		});
		return response.data;
	}

	private createRequest(filters: Array<IFilterItem>): any {
		var request: ISearchRequest = {
			awardingBodies: this.filterValuesForType(filters, FilterType.AwardingBody),
			levels: this.filterValuesForType(filters, FilterType.Levels),
			fundingStreams: this.filterValuesForType(filters, FilterType.FundingStreams),
			searchTerm: this.searchTerm(),
			teachingYears: this.teachingYears()
		};

		return request;
	}

	private filterValuesForType = (filters: Array<IFilterItem>, type: FilterType): Array<string> => filters.filter(f => f.type.toString() === FilterType[type].toString()).map(f => f.key);

	private searchTerm = (): string => (<HTMLInputElement>document.getElementById("autocomplete-overlay")).value;

	private teachingYears = (): Array<string> => new Array(`${(<HTMLSelectElement>document.getElementById("TeachingYears")).value}`);
}

export const searchService = new SearchService();