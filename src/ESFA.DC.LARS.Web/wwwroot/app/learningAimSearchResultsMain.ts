import Vue from "vue";
import store from "./store"
import { debounce } from 'vue-debounce';

import Filters from "../app/Components/filters.vue";
import FilterFeedback from '../app/Components/filterFeedback.vue';
import { filterStoreService } from './Services/filterStoreService';
import { SearchType } from './SearchType';
import { qualificationSearchService } from './Services/qualificationSearchService';
import { ResultsDisplayHelper } from './Helpers/resultsDisplayHelper';
import { constants } from './constants';

const vue = new Vue({
    el: "#resultsApp",
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
                const teachingYears: Array<string> = new Array(`${(<HTMLSelectElement>document.getElementById("TeachingYears"))?.value}`);

                latestRequestId++;

                const getResults = async function (requestId: number) {
                    const response = await qualificationSearchService.getResultsAsync(classScope.$store.state.qualificationFilters, searchTerm, teachingYears);
                    // Only update results if no subsequent requests have been made.
                    if (requestId === latestRequestId) {
                        displayHelper.updateForResponse(response);
                    }
                };
                await getResults(latestRequestId);
            }
        }

        const debouncedCallback = debounce(callback, constants.debounceTime);
        filterStoreService.watchFilters(SearchType.Qualifications, debouncedCallback, this.immediateRefresh, true);
    },
    methods: {
        setImmediateRefreshRequired: function (refreshRequired: boolean) {
            this.immediateRefresh = refreshRequired;
        }
    }
});