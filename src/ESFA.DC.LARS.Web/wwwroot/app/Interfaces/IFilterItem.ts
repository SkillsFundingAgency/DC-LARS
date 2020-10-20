import { FilterType } from '../Enums/FilterType';

export interface IFilterItem {
    key : string;
    value : string;
    type : FilterType;
}