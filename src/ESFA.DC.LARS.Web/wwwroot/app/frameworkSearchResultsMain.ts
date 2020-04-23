import Vue from "vue";
import store from "./store";
import { debounce } from 'vue-debounce';

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
    data() {
        return {
            immediateRefresh: false
        };
    },
    mounted() {
        const classScope = this;
        let latestRequestId = 0;
        const callback = async function () {
            const resultsContainer = classScope.$refs["Results"] as HTMLElement;
            if (resultsContainer) {
                const displayHelper = new ResultsDisplayHelper(resultsContainer, classScope.$refs["ResultsCount"] as HTMLElement, classScope.$refs["ValidationErrors"] as HTMLElement);
                displayHelper.setIsLoading();

                const searchTerm: string = (<HTMLInputElement>document.getElementById("autocomplete-overlay"))?.value;

                latestRequestId++;
                const getResults = async function (requestId: number) {
                    var response = await frameworkSearchService.getResultsAsync(filterStoreService.getSavedFilters(SearchType.Frameworks), searchTerm);
                    // Only update results if no subsequent requests have been made.
                    if (requestId === latestRequestId) {
                        displayHelper.updateForResponse(response);
                    }
                }

                await getResults(latestRequestId);
            }
        }
        const debouncedCallback = debounce(callback, '400ms');
        filterStoreService.watchFilters(SearchType.Frameworks, debouncedCallback, this.immediateRefresh, true);
    },
    methods: {
        setImmediateRefreshRequired: function (refreshRequired: boolean) {
            this.immediateRefresh = refreshRequired;
        }
    }
});