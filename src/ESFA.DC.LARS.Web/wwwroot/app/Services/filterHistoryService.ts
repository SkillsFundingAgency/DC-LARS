import StorageService from '../Services/storageService';
import { constants } from '../constants';

export default class FilterHistoryService {

    private storageService: StorageService;

    constructor() {
        this.storageService = new StorageService(sessionStorage);
    }

    public hasMismatchedFilters(): boolean {
        const storageItem = this.storageService.retrieve(constants.storageKey);
        const sessionFilters = storageItem?.filters;
        const serverFilters = storageItem?.serverFilters;

        if (sessionFilters && serverFilters) {
            const addedFilters = sessionFilters.filter(filter => !serverFilters.find(f => f.key === filter.key && f.type === filter.type));
            const removedFilters = serverFilters.filter(filter => !sessionFilters.find(f => f.key === filter.key && f.type === filter.type));
            return (addedFilters.length + removedFilters.length) > 0;
        }
        return false;
    }
}