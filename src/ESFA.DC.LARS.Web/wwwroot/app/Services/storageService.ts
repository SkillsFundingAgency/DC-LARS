import { IStorageItem, emptyIStorageItem } from '../Interfaces/IStorageItem';
import { IFilterItem } from '../Interfaces/IFilterItem';
import { FilterType } from '../Enums/FilterType';

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

        return emptyIStorageItem();
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
            item.filters.push({ type: FilterType.TeachingYears, key: item.currentAcademicYear, value: '' });
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