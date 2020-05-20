import { IFilterItem } from '../Interfaces/IFilterItem';
import { SearchType } from '../Enums/SearchType';
import store from "../store"

 class FilterStoreService {
    watchFilters(searchType: SearchType, callback: Function, immediate: boolean, deep: boolean ): void {
        store.watch(
            function (state) {
                switch (searchType) {
                    case SearchType.Qualifications:
                        return state.qualificationFilters;
                    case SearchType.Frameworks:
                        return state.frameworkFilters;
                    case SearchType.Units:
                        return state.unitFilters;
                    default:
                        return [];
                }
            },
            function () {
                callback();
            },
            {
                immediate: immediate,
                deep: deep
            });
     }

     updateStore(searchType: SearchType, filters: Array<IFilterItem>): void {
         switch (searchType) {
             case SearchType.Qualifications:
                 store.commit('updateQualificationFilters', filters);
                 break;
             case SearchType.Frameworks:
                 store.commit('updateFrameworkFilters', filters);
                 break;
             case SearchType.Units:
                 store.commit('updateUnitFilters', filters);
                 break;
         }
     }

     getSavedFilters(searchType: SearchType): Array<IFilterItem>{
         switch (searchType) {
             case SearchType.Qualifications:
                 return store.state.qualificationFilters;
             case SearchType.Frameworks:
                 return store.state.frameworkFilters;
             case SearchType.Units:
                 return store.state.unitFilters;
             default:
                 return [];
         }
     }
}

export const filterStoreService = new FilterStoreService();