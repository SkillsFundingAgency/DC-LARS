import { IStorageItem } from '../Interfaces/IStorageItem';

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

        return { searchTerm: '', learnAimRef: '', learningAimTitle: '', learningAimDetailsYear: '', filters: [] };
    }

    store(key: string, value: IStorageItem) {
        this.storage.setItem(key, JSON.stringify(value));
    }

    clear(key: string) {
        this.storage.removeItem(key);
    }

    clearAll() {
        this.storage.clear();
    }
}