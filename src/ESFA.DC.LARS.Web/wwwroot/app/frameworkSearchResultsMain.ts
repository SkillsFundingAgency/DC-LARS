import Vue from "vue";
import store from "./store"

import Filters from "./Components/filters.vue";
import FilterFeedback from './Components/filterFeedback.vue';
import { frameworkSearchService } from './Services/frameworkSearchService';
import { filterStoreService } from './Services/filterStoreService';
import { SearchType } from './SearchType';
import { ResultsDisplayHelper } from './Helpers/resultsDisplayHelper';

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
            const resultsContainer = classScope.$refs["Results"] as HTMLElement;

            if (resultsContainer) {
                const displayHelper = new ResultsDisplayHelper(resultsContainer, classScope.$refs["ResultsCount"] as HTMLElement, classScope.$refs["ValidationErrors"] as HTMLElement);
                displayHelper.setIsLoading();

                const searchTerm: string = (<HTMLInputElement>document.getElementById("autocomplete-overlay"))?.value;
               
                var response = await frameworkSearchService.getResultsAsync(filterStoreService.getSavedFilters(SearchType.Frameworks), searchTerm);

                displayHelper.updateForResponse(response);
            }
        }
        filterStoreService.watchFilters(SearchType.Frameworks, callback, false, true);
    }
});