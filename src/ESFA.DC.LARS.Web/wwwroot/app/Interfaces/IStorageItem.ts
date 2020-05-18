import { IFilterItem } from '../Interfaces/IFilterItem';
import { SearchType } from '../SearchType';

export interface IStorageItem {
    searchTerm: string;
    learnAimRef: string;
    learningAimTitle: string;
    learningAimDetailsYear: string;
    currentAcademicYear: string;
    searchType: SearchType;
    frameworkCode: string;
    programType: string;
    pathwayCode: string;
    filters: IFilterItem[];
}