import { IFilterItem } from '../Interfaces/IFilterItem';
import store from "../store"

export abstract class AbstractFilterStoreStrategy {
    watchFilters(callback: Function, immediate: boolean, deep: boolean): void {
        const classScope = this;
        store.watch(
            function (state) {
                return classScope.getSavedFilters();
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
