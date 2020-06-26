import { IFilterItem } from '../Interfaces/IFilterItem';
import store from "../store"

export abstract class AbstractFilterStoreStrategy {
    watchFilters(callback: () => void, immediate: boolean, deep: boolean): void {
        store.watch(
            () => {
                return this.getSavedFilters();
            },
            function () {
                callback();
            },
            {
                immediate: immediate,
                deep: deep
            });
    }

    abstract updateStore(filters: Array<IFilterItem>): void;
    abstract getSavedFilters(): Array<IFilterItem>;
}
