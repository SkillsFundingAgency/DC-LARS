import { IFilterItem } from '../Interfaces/IFilterItem';
import { FilterType } from '../Enums/FilterType';

class FilterService {
    removeFilterFromArray(filters: Array<IFilterItem>, item: IFilterItem): Array<IFilterItem> {
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
            let type1Ordinal = 0;
            let type2Ordinal = 0;
            const keys = Object.keys(FilterType);

            keys.forEach((key, i) => {
                if (key === FilterType[f1.type]) type1Ordinal = i;
                if (key === FilterType[f2.type]) type2Ordinal = i;
            });
            
            if (type1Ordinal > type2Ordinal) {
                return 1;
            }

            if (type1Ordinal < type2Ordinal) {
                return -1;
            }

            if (type1Ordinal === type2Ordinal) {
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
    filterValuesForType = (filters: Array<IFilterItem>, type: FilterType): Array<string> => filters.filter(f => f.type.toString() === FilterType[type].toString()).map(f => f.key);
}

export const filterService = new FilterService();