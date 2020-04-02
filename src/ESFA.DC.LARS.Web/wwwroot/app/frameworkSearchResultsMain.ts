import Vue from "vue";
import store from "./store"

import Filters from "../app/Components/filters.vue";
import FilterFeedback from '../app/Components/filterFeedback.vue';
import { frameworkSearchService } from './Services/frameworkSearchService';
import { filterStoreService } from './Services/filterStoreService';
import { SearchType } from './SearchType';

let vue = new Vue({
    el: "#frameworkResults",
    store,
    components: {
        'filter-feedback': FilterFeedback,
        'filters': Filters
    },
    mounted() {
        const classScope = this;
        const callback = async function () {
            const resultsContainer = <HTMLElement>classScope.$refs["Results"];
            const resultCount = <HTMLElement>classScope.$refs["ResultsCount"];
            const validationErrorContainer = <HTMLElement>classScope.$refs["ValidationErrors"];
            const searchTerm: string = (<HTMLInputElement>document.getElementById("autocomplete-overlay")).value;

            resultsContainer.innerHTML = "Loading";

            var response = await frameworkSearchService.getResultsAsync(filterStoreService.getSavedFilters(SearchType.Frameworks), searchTerm);

            resultsContainer.innerHTML = response.data;
            resultCount.innerHTML = `${response.count} results`;
            validationErrorContainer.innerHTML = response.validationErrors.map(v => `<li class="govuk-error-message">${v}</li>`).join();
        }

        filterStoreService.watchFilters(SearchType.Frameworks ,callback, false, true);
    }
});