import { IFilterItem } from '../Interfaces/IFilterItem';
import { SearchType } from '../Enums/SearchType';

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
}