import { IFilterItem } from '../Interfaces/IFilterItem';
import { SearchType } from '../Enums/SearchType';
import { Page } from '../Enums/Page';

export interface IStorageItem {
    searchTerm: string;
    learnAimRef: string;
    learningAimTitle: string;
    standardName: string;
    standardCode: string;
    currentAcademicYear: string;
    searchType: SearchType;
    frameworkCode: string;
    programType: string;
    pathwayCode: string;
    filters: IFilterItem[];
    serverFilters: IFilterItem[];
    page: Page;
    hasFilterMismatch: boolean
}

export function emptyIStorageItem(): IStorageItem {
    return {
        searchTerm: '', learnAimRef: '', learningAimTitle: '', standardName: '', standardCode: '', page: Page.Home, hasFilterMismatch: false,
        currentAcademicYear: '', searchType: SearchType.Qualifications, frameworkCode: '', programType: '', pathwayCode: '', filters: [], serverFilters: []
    };
}