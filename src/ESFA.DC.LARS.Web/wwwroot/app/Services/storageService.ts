import { IStorageItem } from '../Interfaces/IStorageItem';
import { IFilterItem } from '../Interfaces/IFilterItem';

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

        return { searchTerm: '', learnAimRef: '', learningAimTitle: '', learningAimDetailsYear: '', teachingYear: '', filters: [] };
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
        const item = this.retrieve(key);
        item.filters = [];
        this.store(key, item);
    }

    clear(key: string) : void {
        this.storage.removeItem(key);
    }

    clearAll() : void {
        this.storage.clear();
    }
}