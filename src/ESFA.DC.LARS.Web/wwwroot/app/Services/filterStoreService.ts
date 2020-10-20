import { IFilterItem } from '../Interfaces/IFilterItem';
import { SearchType } from '../Enums/SearchType';
import { filterStoreStrategyManager } from '../FilterStores/filterstoreStrategyManager';
import { AbstractFilterStoreStrategy } from '../FilterStores/abstractFilterStoreStrategy';
import { FilterType } from '../Enums/FilterType';
import { IStorageItem } from '../Interfaces/IStorageItem';

export class FilterStoreService {

    private _filterStoreStrategy: AbstractFilterStoreStrategy;
    
    constructor(searchType: SearchType) {
        this._filterStoreStrategy = filterStoreStrategyManager.getFilterStoreStrategy(searchType);
    }

    public watchFilters(callback: () => void, immediate: boolean, deep: boolean): void {
        this._filterStoreStrategy.watchFilters(callback, immediate, deep);
    }

    public updateStore(filters: Array<IFilterItem>): void {
        this._filterStoreStrategy.updateStore(filters);
    }

    public getSavedFilters(): Array<IFilterItem>{
        return this._filterStoreStrategy.getSavedFilters();
    }

    public getFiltersForNewSearch(newSearchType: SearchType, storageItem: IStorageItem): Array<IFilterItem> {
        let updatedFilters: Array<IFilterItem> = [];
        // If new search has teaching years then keep exisiting teaching year
        // filter if present or use current academic year if not.
        if (this.canHaveTeachingYearFilter(newSearchType)) {
            const currentFilters = this.getSavedFilters();
            if (currentFilters.some(f => f.type === FilterType.TeachingYears)) {
                updatedFilters = currentFilters.filter(f => f.type === FilterType.TeachingYears);
            } else {
                updatedFilters.push({ type: FilterType.TeachingYears, key: storageItem.currentAcademicYear, value: '' });
            }
        }
        return updatedFilters;
    }

    private canHaveTeachingYearFilter(searchType: SearchType) {
        return (searchType === SearchType.Qualifications || searchType === SearchType.Units);
    } 

}