import { IFilterItem, FilterType } from '../Interfaces/IFilterItem';
import { SearchType } from '../SearchType';

 class FilterService {
    removeFilterFromArray(filters: Array<IFilterItem>, item: IFilterItem) : Array<IFilterItem> {
        const found = this.findFilterItem(item.key, item.type, filters);

        if (found !== undefined) {
            const index = filters.indexOf(found);
            if (index > -1) {
                filters.splice(index, 1);
            }
        }
        return filters;
    }

    findFilterItem(key: string, type: FilterType, filters: Array<IFilterItem>): IFilterItem | undefined {
        const found = filters.find(function (filter) {
            return filter.key === key && filter.type === type;
        });

        return found;
     }

    findFilterItemByType(type: FilterType, filters: Array<IFilterItem>): IFilterItem | undefined {
        const found = filters.find(function (filter) {
            return filter.type === type;
        });

        return found;
     }

    sortFilters(filters: Array<IFilterItem>): Array<IFilterItem> {
        return filters.sort((f1, f2) => {
            if (f1.type > f2.type) {
                return 1;
            }

            if (f1.type < f2.type) {
                return -1;
            }

            if (f1.type === f2.type) {
                if (f1.value > f2.value) {
                    return 1;
                }

                if (f1.value < f2.value) {
                    return -1;
                }
            }

            return 0;
        });
    }
    watchFilters(classScope: Vue, searchType: SearchType, callback: Function, immediate: boolean, deep: boolean ) {
        classScope.$store.watch(
            function (state) {
                if (searchType === SearchType.Qualifications) {
                    return state.qualificationFilters;
                }

                if (searchType === SearchType.Frameworks) {
                    return state.frameworkFilters;
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
     updateStore(classScope: Vue, searchType: SearchType, filters: Array<IFilterItem>) {
         if (searchType === SearchType.Qualifications) {
             classScope.$store.commit('updateQualificationFilters', filters);
         }

         if (searchType === SearchType.Frameworks) {
             classScope.$store.commit('updateFrameworkFilters', filters);
         }
     }
     savedFilters(classScope: Vue, searchType: SearchType): Array<IFilterItem>{
         if (searchType === SearchType.Qualifications) {
             return classScope.$store.state.qualificationFilters;
         }

         if (searchType === SearchType.Frameworks) {
             return classScope.$store.state.frameworkFilters;
         }

         return [];
     }
     filterValuesForType = (filters: Array<IFilterItem>, type: FilterType): Array<string> => filters.filter(f => f.type.toString() === FilterType[type].toString()).map(f => f.key);
}

export const filterService = new FilterService();