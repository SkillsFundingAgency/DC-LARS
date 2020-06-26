import { IFilterItem } from '../Interfaces/IFilterItem';
import store from "../store"
import { AbstractFilterStoreStrategy } from './abstractFilterStoreStrategy';

class QualificationsFilterStoreStrategy extends AbstractFilterStoreStrategy {
    updateStore(filters: IFilterItem[]): void {
        store.commit('updateQualificationFilters', filters);
    }
    getSavedFilters(): IFilterItem[] {
        return store.state.qualificationFilters;
    }
}

export const qualificationsFilterStoreStrategy = new QualificationsFilterStoreStrategy();