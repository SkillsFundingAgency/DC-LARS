import { IFilterItem } from '../Interfaces/IFilterItem';
import store from "../store"
import { AbstractFilterStoreStrategy } from './abstractFilterStoreStrategy';

class StandardsFilterStoreStrategy extends AbstractFilterStoreStrategy {
    updateStore(filters: IFilterItem[]): void {
        store.commit('updateStandardFilters', filters);
    }
    getSavedFilters(): IFilterItem[] {
        return store.state.standardFilters;
    }
}

export const standardsFilterStoreStrategy = new StandardsFilterStoreStrategy();