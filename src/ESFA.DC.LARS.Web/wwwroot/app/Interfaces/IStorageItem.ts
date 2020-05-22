import { IFilterItem } from '../Interfaces/IFilterItem';
import { SearchType } from '../Enums/SearchType';
import { Page } from '../Enums/Page';

export interface IStorageItem {
    searchTerm: string;
    learnAimRef: string;
    learningAimTitle: string;
    currentAcademicYear: string;
    searchType: SearchType;
    frameworkCode: string;
    programType: string;
    pathwayCode: string;
    filters: IFilterItem[];
    page: Page
}

export function emptyIStorageItem(): IStorageItem {
    return {
        searchTerm: '', learnAimRef: '', learningAimTitle: '', page: Page.Home,
        currentAcademicYear: '', searchType: SearchType.Qualifications, frameworkCode: '', programType: '', pathwayCode: '', filters: []
    };
}