import { IFilterItem } from '../Interfaces/IFilterItem';
import store from "../store"
import { AbstractFilterStoreStrategy } from './abstractFilterStoreStrategy';

class FrameworksFilterStoreStrategy extends AbstractFilterStoreStrategy {
    updateStore(filters: IFilterItem[]): void {
        store.commit('updateFrameworkFilters', filters);
    }
    getSavedFilters(): IFilterItem[] {
        return store.state.frameworkFilters;
    }
}

export const frameworksFilterStoreStrategy = new FrameworksFilterStoreStrategy();