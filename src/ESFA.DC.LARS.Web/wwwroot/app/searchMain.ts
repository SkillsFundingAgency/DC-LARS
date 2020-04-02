import Vue from "vue";

import { IFilterItem, FilterType } from './Interfaces/IFilterItem';
import StorageService from "./Services/storageService";

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

            const awardingBodyFilterElement = document.getElementById('AwardingBody');
            if (awardingBodyFilterElement) {
                const filter: IFilterItem = {
                    key: '',
                    value: awardingBodyFilterElement.innerHTML,
                    type: FilterType.AwardingBody
                };
                storageItem.filters.push(filter);
            }

            const levelElement = document.getElementById('Level') as HTMLSelectElement;
            if (levelElement) {
                const filter: IFilterItem = {
                    key: levelElement.options[levelElement.selectedIndex].value,
                    value: levelElement.options[levelElement.selectedIndex].text,
                    type: FilterType.Levels
                };
                storageItem.filters.push(filter);
            }

            const teachingYearElement = document.getElementById('TeachingYear') as HTMLSelectElement;
            if (teachingYearElement) {
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