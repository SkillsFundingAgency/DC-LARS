import { IFilterItem, FilterType } from '../../app/Interfaces/IFilterItem';
import StorageService from '../Services/storageService';
import { IStorageItem } from '../Interfaces/IStorageItem';
import { filterStoreService } from '../Services/filterStoreService';
import { SearchType } from '../SearchType';

export default class FilterHistoryService {

    private storageService: StorageService;
    private storageKey = 'sessionData';
    private searchType!: SearchType;

    constructor(searchType: SearchType) {
        this.storageService = new StorageService(sessionStorage);
        this.searchType = searchType;
    }

    get savedfilters(): Array<IFilterItem> {
        return [...filterStoreService.getSavedFilters(this.searchType)];
    };


    public hasUnappliedFilters(): boolean {
        const filters = this.storageService.retrieve(this.storageKey).filters;
        let unappliedFilters = false;

        if (filters) {
            for (const filter of filters) {
                if (filter.type === FilterType.TeachingYears) {
                    const teachingYearElement = document.querySelector(`select#TeachingYears option[value='${filter.key}']`) as HTMLOptionElement;
                    if (teachingYearElement && !teachingYearElement.selected) {
                        unappliedFilters = true;
                    }
                }
                else {
                    const checkBox = document.querySelector(`input[type='checkbox'][name='${filter.type}'][value='${filter.key}']`) as HTMLInputElement;
                    if (checkBox && !checkBox.checked) {
                        unappliedFilters = true;
                    }
                }
            }
        }

        return unappliedFilters;
    }

    public getFilterHistory(): Array<IFilterItem> {
        const filters = this.storageService.retrieve(this.storageKey).filters;

        const sessionData = this.storageService.retrieve(this.storageKey) as IStorageItem;
        const storeFilters = new Array<IFilterItem>();

        if (sessionData && filters) {

            for (const filter of filters) {

                //empty homepage filter
                if (filter.key === '' && filter.value === '') {
                    continue;
                }

                if (filter.type !== FilterType.TeachingYears) {
                    let checkbox!: HTMLInputElement;

                    //try to handle free text input from home page :(
                    if (filter.key === '') {
                        const checkboxes = document.querySelectorAll("input[type='checkbox']");

                        checkboxes.forEach(cbox => {
                            const cboxElement = cbox as HTMLInputElement;
                            const description = cbox.getAttribute('data-description');

                            if (cboxElement.value.includes(filter.value) || (description && description.includes(filter.value))) {
                                checkbox = cboxElement;
                            }
                        });
                    }
                    else {
                        checkbox = document.querySelector(`input[type='checkbox'][name='${filter.type}'][value='${filter.key}']`) as HTMLInputElement;
                    }

                    if (checkbox && !this.storeContainsFilter(filter)) {
                        storeFilters.push(filter);
                    }
                }
                else if (filter.type === FilterType.TeachingYears) {
                    const teachingYearElement = document.querySelector(`select#TeachingYears option[value='${filter.key}']`) as HTMLOptionElement;
                    if (teachingYearElement && !this.storeContainsFilter(filter)) {
                        storeFilters.push(filter);
                    }
                }
            }
        }

        return storeFilters;
    }

    private storeContainsFilter(filter: IFilterItem): boolean {
        return this.savedfilters.includes(filter);
    }
}