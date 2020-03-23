import { IFilterItem } from '../Interfaces/IFilterItem';

export interface IStorageItem {
    searchTerm: string;
    learnAimRef: string;
    learningAimTitle: string;
    learningAimDetailsYear: string;
    filters: IFilterItem[];
}