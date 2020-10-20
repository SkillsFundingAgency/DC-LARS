import { IFilterItem } from '../Interfaces/IFilterItem';
import store from "../store"
import { AbstractFilterStoreStrategy } from './abstractFilterStoreStrategy';

class UnitsFilterStoreStrategy extends AbstractFilterStoreStrategy {
    updateStore(filters: IFilterItem[]): void {
        store.commit('updateUnitFilters', filters);
    }
    getSavedFilters(): IFilterItem[] {
        return store.state.unitFilters;
    }
}

export const unitsFilterStoreStrategy = new UnitsFilterStoreStrategy();