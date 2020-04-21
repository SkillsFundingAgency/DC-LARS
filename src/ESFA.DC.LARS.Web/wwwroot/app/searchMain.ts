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
        updateFilters: function(searchType: string) {
            const storageService = new StorageService(sessionStorage);
            const storageItem = storageService.retrieve('');

            if (searchType === 'qualifications') {
                const awardingBodyFilterElement = document.getElementById('AwardingBody') as HTMLInputElement;
                if (awardingBodyFilterElement && awardingBodyFilterElement.value) {
                    const filter: IFilterItem = {
                        key: '',
                        value: awardingBodyFilterElement.value,
                        type: FilterType.AwardingBodies
                    };
                    storageItem.filters.push(filter);
                }

                const levelElement = document.getElementById('Level') as HTMLSelectElement;
                if (levelElement && levelElement.value) {
                    const filter: IFilterItem = {
                        key: levelElement.options[levelElement.selectedIndex].value,
                        value: levelElement.options[levelElement.selectedIndex].text,
                        type: FilterType.Levels
                    };
                    storageItem.filters.push(filter);
                }

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