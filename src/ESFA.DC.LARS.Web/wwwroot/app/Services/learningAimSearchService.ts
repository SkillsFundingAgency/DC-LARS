import axios from 'axios'
import qs from 'qs';

import { IFilterItem } from '../Interfaces/IFilterItem';
import { FilterType } from '../Enums/FilterType';
import { IQualificationsSearchRequest } from '../Interfaces/IQualificationsSearchRequest';
import { ISearchResults } from '../Interfaces/ISearchResults';
import { filterService } from '../Services/filterService';

class LearningAimSearchService {

	async getQualificationsResultsAsync(filters: Array<IFilterItem>, searchTerm: string, teachingYears: Array<string>): Promise<ISearchResults>{
		const response = await axios.get<ISearchResults>('../LearningAimSearchResult/Results', {
			params: this.createRequest(filters, searchTerm, teachingYears), paramsSerializer: p =>  qs.stringify(p)
		});
		return response.data;
	}

	async getUnitsResultsAsync(filters: Array<IFilterItem>, searchTerm: string, teachingYears: Array<string>): Promise<ISearchResults> {
		const response = await axios.get<ISearchResults>('../UnitSearchResult/Results', {
			params: this.createRequest(filters, searchTerm, teachingYears), paramsSerializer: p => qs.stringify(p)
		});
		return response.data;
	}

	private createRequest(filters: Array<IFilterItem>, searchTerm: string, teachingYears: Array<string>): IQualificationsSearchRequest {
		const request: IQualificationsSearchRequest = {
			awardingBodies: filterService.filterValuesForType(filters, FilterType.AwardingBodies),
			levels: filterService.filterValuesForType(filters, FilterType.Levels),
			fundingStreams: filterService.filterValuesForType(filters, FilterType.FundingStreams),
			searchTerm: searchTerm,
			teachingYears: teachingYears
		};

		return request;
	}
}

export const learningAimSearchService = new LearningAimSearchService();