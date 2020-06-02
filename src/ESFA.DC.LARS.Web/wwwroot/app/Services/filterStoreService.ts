import { IFilterItem } from '../Interfaces/IFilterItem';
import { SearchType } from '../Enums/SearchType';
import { filterStoreStrategyManager } from '../FilterStores/filterstoreStrategyManager';
import { AbstractFilterStoreStrategy } from '../FilterStores/abstractFilterStoreStrategy';

export class FilterStoreService {

    private _filterStoreStrategy: AbstractFilterStoreStrategy;
    
    constructor(searchType: SearchType) {
        this._filterStoreStrategy = filterStoreStrategyManager.getFilterStoreStrategy(searchType);
    }

    watchFilters(callback: Function, immediate: boolean, deep: boolean): void {
        this._filterStoreStrategy.watchFilters(callback, immediate, deep);
    }

    updateStore(filters: Array<IFilterItem>): void {
        this._filterStoreStrategy.updateStore(filters);
    }

    getSavedFilters(): Array<IFilterItem>{
        return this._filterStoreStrategy.getSavedFilters();
    }
}