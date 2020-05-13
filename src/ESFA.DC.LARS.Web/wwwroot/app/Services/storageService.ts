import { IStorageItem } from '../Interfaces/IStorageItem';
import { IFilterItem, FilterType } from '../Interfaces/IFilterItem';
import { SearchType } from '../SearchType';

export default class StorageService {

    private readonly storage: Storage;

    constructor(storageType: Storage)
    {
        this.storage = storageType;
    }

    retrieve(key: string): IStorageItem
    {
        const item = this.storage.getItem(key);

        if (item && item !== 'undefined') {
            return JSON.parse(item) as IStorageItem;
        }

        return {
            searchTerm: '', learnAimRef: '', learningAimTitle: '', learningAimDetailsYear: '',
            teachingYear: '', searchType: SearchType.Qualifications, frameworkCode: '', programType: '', pathwayCode: '', filters: []
        };
    }

    store(key: string, value: IStorageItem) : void {
        this.storage.setItem(key, JSON.stringify(value));
    }

    updateFilters(key: string, filters: Array<IFilterItem>) : void {
        const item = this.retrieve(key);

        item.filters = filters;
        this.store(key, item);
    }

    clearFilters(key: string) : void {
        const item = this.retrieve(key) as IStorageItem;
        const hasTeachingYear = item.filters.some(f => f.type === FilterType.TeachingYears);
        item.filters = [];
        // Set default teaching year if required.
        if (hasTeachingYear) {
            item.filters.push({ type: FilterType.TeachingYears, key: item.teachingYear, value:''})
        }
        this.store(key, item);
    }

    clear(key: string) : void {
        this.storage.removeItem(key);
    }

    clearAll() : void {
        this.storage.clear();
    }
}