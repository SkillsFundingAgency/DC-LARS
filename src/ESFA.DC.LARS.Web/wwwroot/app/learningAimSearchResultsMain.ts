import Vue from "vue";
import store from "./store"

import Filters from "./Components/filters.vue";
import FilterFeedback from './Components/filterFeedback.vue';
import { qualificationSearchService } from './Services/qualificationSearchService';
import { filterStoreService } from './Services/filterStoreService';
import { SearchType } from './SearchType';
import { ResultsDisplayHelper } from './Helpers/resultsDisplayHelper';

const vue = new Vue({
    el: "#resultsApp",
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
                displayHelper.SetIsLoading();

                const searchTerm: string = (<HTMLInputElement>document.getElementById("autocomplete-overlay"))?.value;
                const teachingYears: Array<string> = new Array(`${(<HTMLSelectElement>document.getElementById("TeachingYears"))?.value}`);

                var response = await qualificationSearchService.getResultsAsync(classScope.$store.state.qualificationFilters, searchTerm, teachingYears);

                displayHelper.UpdateForResponse(response);
            }
        }
        filterStoreService.watchFilters(SearchType.Qualifications, callback, false, true);
    }
});