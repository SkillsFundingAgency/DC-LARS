import { IFilterItem, FilterType } from '../Interfaces/IFilterItem';

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

    watchQualificationFilters(classScope: Vue, callback : Function) {
        classScope.$store.watch(
            function (state) {
                return state.qualificationFilters;
            },
            function () {
                callback()
            },
            {
                immediate: true,
                deep: true
            });
    }
}

export const filterService = new FilterService();