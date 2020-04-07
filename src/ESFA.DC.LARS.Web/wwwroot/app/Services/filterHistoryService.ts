﻿import { IFilterItem, FilterType } from '../../app/Interfaces/IFilterItem';
import StorageService from '../Services/storageService';
import { IStorageItem } from '../Interfaces/IStorageItem';
import { filterStoreService } from '../Services/filterStoreService';
import { SearchType } from '../SearchType';

export default class FilterHistoryService {

    private storageService : StorageService;
    private storageKey = 'sessionData';
    private searchType!: SearchType;

    constructor(searchType : SearchType) {
        this.storageService = new StorageService(sessionStorage);
        this.searchType = searchType;
    }

    get savedfilters(): Array<IFilterItem> {
        return [...filterStoreService.getSavedFilters(this.searchType)];
    };

    public getFilterHistory(): Array<IFilterItem> {
        const filters = this.storageService.retrieve(this.storageKey).filters;

        const sessionData = this.storageService.retrieve(this.storageKey) as IStorageItem;
        const storeFilters = new Array<IFilterItem>();

        if (sessionData) {
            if (filters) {
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
                            checkbox = document.querySelector("input[type='checkbox'][value='" + filter.key + "']") as HTMLInputElement;
                        }

                        if (checkbox) {
                            if (!this.storeContainsFilter(filter)) {
                                storeFilters.push(filter);
                            }
                        }
                    }
                    else { //handle teaching year select
                        const teachingYearElement = document.getElementById(filter.key) as HTMLOptionElement;

                        if (teachingYearElement) {
                            if (!this.storeContainsFilter(filter)) {
                                storeFilters.push(filter);
                            }
                        }
                    }
                }
            }
        }

        return storeFilters;
    }

    private storeContainsFilter(filter : IFilterItem): boolean {
        return this.savedfilters.includes(filter);
    }
}