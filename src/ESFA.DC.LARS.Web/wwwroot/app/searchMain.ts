import Vue from "vue";

import { IFilterItem, FilterType } from './Interfaces/IFilterItem';
import StorageService from "./Services/storageService";

const vue = new Vue({
    el: "#homeApp",
    mounted() {
        (<any>window).GOVUKFrontend.initAll();

        const storageService = new StorageService(sessionStorage);
        storageService.clearAll();
        (<any>window).GOVUKFrontend.initAll();
    },
    methods: {
        updateFilters: function() {
            const storageService = new StorageService(sessionStorage);
            const storageItem = storageService.retrieve('');

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

            const teachingYearElement = document.getElementById('TeachingYear') as HTMLSelectElement;
            if (teachingYearElement && teachingYearElement.value) {
                const filter: IFilterItem = {
                    key: teachingYearElement.options[teachingYearElement.selectedIndex].value,
                    value: teachingYearElement.options[teachingYearElement.selectedIndex].text,
                    type: FilterType.TeachingYears
                };
                storageItem.filters.push(filter);
            }

            storageService.store('sessionData', storageItem);
        }
    }
});