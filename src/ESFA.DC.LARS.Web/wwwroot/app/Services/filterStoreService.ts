import { IFilterItem } from '../Interfaces/IFilterItem';
import { SearchType } from '../SearchType';
import store from "../store"

 class FilterStoreService {
    watchFilters(searchType: SearchType, callback: Function, immediate: boolean, deep: boolean ) {
        store.watch(
            function (state) {
                if (searchType === SearchType.Qualifications) {
                    return state.qualificationFilters;
                }

                if (searchType === SearchType.Frameworks) {
                    return state.frameworkFilters;
                }
                return [];
            },
            function () {
                callback()
            },
            {
                immediate: immediate,
                deep: deep
            });
     }
     updateStore(searchType: SearchType, filters: Array<IFilterItem>) {
         if (searchType === SearchType.Qualifications) {
             store.commit('updateQualificationFilters', filters);
         }

         if (searchType === SearchType.Frameworks) {
             store.commit('updateFrameworkFilters', filters);
         }
     }
     getSavedFilters(searchType: SearchType): Array<IFilterItem>{
         if (searchType === SearchType.Qualifications) {
             return store.state.qualificationFilters;
         }

         if (searchType === SearchType.Frameworks) {
             return store.state.frameworkFilters;
         }

         return [];
     }
}

export const filterStoreService = new FilterStoreService();