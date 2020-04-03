import { IFilterItem } from '../Interfaces/IFilterItem';
import { SearchType } from '../SearchType';
import store from "../store"

 class FilterStoreService {
    watchFilters(searchType: SearchType, callback: Function, immediate: boolean, deep: boolean ) {
        store.watch(
            function (state) {
                switch (searchType) {
                    case SearchType.Qualifications:
                        return state.qualificationFilters;
                    case SearchType.Frameworks:
                        return state.frameworkFilters;
                    default:
                        return [];
                }
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
         switch (searchType) {
             case SearchType.Qualifications:
                 store.commit('updateQualificationFilters', filters);
             case SearchType.Frameworks:
                 store.commit('updateFrameworkFilters', filters);
         }
     }
     getSavedFilters(searchType: SearchType): Array<IFilterItem>{
         switch (searchType) {
             case SearchType.Qualifications:
                 return store.state.qualificationFilters;
             case SearchType.Frameworks:
                 return store.state.frameworkFilters;
             default:
                 return [];
         }
     }
}

export const filterStoreService = new FilterStoreService();