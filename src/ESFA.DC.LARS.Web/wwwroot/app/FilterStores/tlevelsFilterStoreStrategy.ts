import { IFilterItem } from '../Interfaces/IFilterItem';
import store from "../store"
import { AbstractFilterStoreStrategy } from './abstractFilterStoreStrategy';

class TLevelsFilterStoreStrategy extends AbstractFilterStoreStrategy {
    updateStore(filters: IFilterItem[]): void {
        store.commit('updateTLevelFilters', filters);
    }
    getSavedFilters(): IFilterItem[] {
        return store.state.tlevelFilters;
    }
}

export const tlevelsFilterStoreStrategy = new TLevelsFilterStoreStrategy();