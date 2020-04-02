import Vue from "vue";
import store from "./store"

import Filters from "../app/Components/filters.vue";
import FilterFeedback from '../app/Components/filterFeedback.vue';
import { qualificationSearchService } from './Services/qualificationSearchService';
import { filterStoreService } from './Services/filterStoreService';
import { SearchType } from './SearchType';

const vue = new Vue({
    el: "#resultsApp",
    store,
    components: {
        'filter-feedback': FilterFeedback,
        'filters': Filters
    },
    mounted() {
        const classScope = this;
        
        const callback = async function() {
            const resultsContainer = <HTMLElement>classScope.$refs["Results"];
            const resultCount = <HTMLElement>classScope.$refs["ResultsCount"];
            const validationErrorContainer = <HTMLElement>classScope.$refs["ValidationErrors"];
            const searchTerm : string = (<HTMLInputElement>document.getElementById("autocomplete-overlay")).value;
            const teachingYears : Array<string> = new Array(`${(<HTMLSelectElement>document.getElementById("TeachingYears")).value}`);

            resultsContainer.innerHTML = "Loading";

            var response = await qualificationSearchService.getResultsAsync(classScope.$store.state.qualificationFilters, searchTerm, teachingYears);

            resultsContainer.innerHTML = response.data;
            resultCount.innerHTML = `${response.count} results`;
            validationErrorContainer.innerHTML = response.validationErrors
                .map(error => `<li class="govuk-error-message">${error}</li>`).join();
        }

        filterStoreService.watchFilters(SearchType.Qualifications ,callback, false, true);
    }
});