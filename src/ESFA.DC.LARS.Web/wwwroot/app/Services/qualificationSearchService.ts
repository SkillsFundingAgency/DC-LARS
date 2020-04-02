import axios from 'axios'
import qs from 'qs';

import { IFilterItem, FilterType } from '../Interfaces/IFilterItem';
import { IQualificationsSearchRequest } from '../Interfaces/IQualificationsSearchRequest';
import { ISearchResults } from '../Interfaces/ISearchResults';
import { filterService } from '../Services/filterService';

class QualificationSearchService {

	async getResultsAsync(filters: Array<IFilterItem>, searchTerm: string, teachingYears: Array<string>): Promise<ISearchResults>{
		var response = await axios.get<ISearchResults>('../LearningAimSearchResult/Results', {
			params: this.createRequest(filters, searchTerm, teachingYears), paramsSerializer: p =>  qs.stringify(p)
		});
		return response.data;
	}

	private createRequest(filters: Array<IFilterItem>, searchTerm: string, teachingYears: Array<string>): any {
		var request: IQualificationsSearchRequest = {
			awardingBodies: filterService.filterValuesForType(filters, FilterType.AwardingBody),
			levels: filterService.filterValuesForType(filters, FilterType.Levels),
			fundingStreams: filterService.filterValuesForType(filters, FilterType.FundingStreams),
			searchTerm: searchTerm,
			teachingYears: teachingYears
		};

		return request;
	}
}

export const qualificationSearchService = new QualificationSearchService();