import { IFilterItem } from '../Interfaces/IFilterItem';

export interface IStorageItem {
    searchTerm: string;
    learnAimRef: string;
    learningAimTitle: string;
    learningAimDetailsYear: string;
    teachingYear: string;
    frameworkSearch: boolean;
    frameworkCode: string;
    programType: string;
    pathwayCode: string;
    filters: IFilterItem[];
}