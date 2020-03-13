import {IFilterItem} from '../../app/Interfaces/IFilterItem';

export interface ISearchFilters {
    levels : Array<IFilterItem>;
    awardingBodies : Array<IFilterItem>;
    teachingYears : Array<IFilterItem>;
    fundingStreams : Array<IFilterItem>;
}