import Vue from "vue";

import { IFilterItem } from './Interfaces/IFilterItem';
import StorageService from "./Services/storageService";
import { constants } from './constants';
import { FilterType } from './Enums/FilterType';

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
            const teachingYearElement = document.getElementById('TeachingYear') as HTMLInputElement;

            if (teachingYearElement && teachingYearElement.value) {
                storageItem.currentAcademicYear = teachingYearElement.value;
                if (searchType === 'Qualifications' || searchType === 'Units') {
                    const filter: IFilterItem = {
                        key: teachingYearElement.value,
                        value: '',
                        type: FilterType.TeachingYears
                    };
                    storageItem.filters.push(filter);
                }
                else {
                    teachingYearElement.value = '';
                }
            }

            storageService.store(constants.storageKey, storageItem);
        }
    }
});