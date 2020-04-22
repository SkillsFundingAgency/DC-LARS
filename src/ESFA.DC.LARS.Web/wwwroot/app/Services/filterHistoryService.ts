import { IFilterItem } from '../../app/Interfaces/IFilterItem';
import StorageService from '../Services/storageService';
import { constants } from '../constants';
declare var serverFilters: IFilterItem[];

export default class FilterHistoryService {

    private storageService: StorageService;

    constructor() {
        this.storageService = new StorageService(sessionStorage);
    }

    get serverFilters(): Array<IFilterItem> {
        return [...serverFilters]; 
    }

    public hasMismatchedFilters(): boolean {
        const sessionFilters = this.storageService.retrieve(constants.storageKey)?.filters;

        if (sessionFilters && serverFilters) {
            const addedFilters = sessionFilters.filter(filter => !serverFilters.find(f => f.key === filter.key && f.type === filter.type));
            const removedFilters = serverFilters.filter(filter => !sessionFilters.find(f => f.key === filter.key && f.type === filter.type));
            return (addedFilters.length + removedFilters.length) > 0;
        }
        return false;
    }
}