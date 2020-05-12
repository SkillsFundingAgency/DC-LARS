import Vue from "vue";

import { IFilterItem, FilterType } from './Interfaces/IFilterItem';
import StorageService from "./Services/storageService";
import { constants } from './constants';
import { SearchType } from "./SearchType";

const vue = new Vue({
    el: "#homeApp",
    mounted() {
        const storageService = new StorageService(sessionStorage);
        storageService.clearAll();
        (<any>window).GOVUKFrontend.initAll();
    },
    methods: {
        updateFilters: function() {
            const storageService = new StorageService(sessionStorage);
            const storageItem = storageService.retrieve('');
            const searchTypeElement = document.getElementById('searchType') as HTMLSelectElement;
            const searchType = searchTypeElement.options[searchTypeElement.selectedIndex].text;
           
            if (searchType === 'Qualifications' || searchType === 'Units') {

               const teachingYearElement = document.getElementById('TeachingYear') as HTMLInputElement;

                if (teachingYearElement && teachingYearElement.value) {
                    const filter: IFilterItem = {
                        key: teachingYearElement.value,
                        value: '',
                        type: FilterType.TeachingYears
                    };
                    storageItem.filters.push(filter);
                }
            }

            storageService.store(constants.storageKey, storageItem);
        }
    }
});