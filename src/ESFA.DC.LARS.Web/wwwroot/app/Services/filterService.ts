import { IFilterItem } from '../Interfaces/IFilterItem';

export default class FilterService {
    removeFilterFromArray(filters: Array<IFilterItem>, item: IFilterItem) : Array<IFilterItem> {
        const found = filters.find(function (filter) {
            return filter.key === item.key && filter.type === item.type;
        });

        if (found !== undefined) {
            const index = filters.indexOf(found);
            if (index > -1) {
                filters.splice(index, 1);
            }
        }
        return filters;
    }

    sortFilters(filters: Array<IFilterItem>): Array<IFilterItem> {
        return filters.sort((f1, f2) => {
            if (f1.type < f2.type) {
                return 1;
            }

            if (f1.type > f2.type) {
                return -1;
            }

            if (f1.value > f2.value) {
                return 1;
            }

            if (f1.value < f2.value) {
                return -1;
            }

            return 0;
        });
    }
}