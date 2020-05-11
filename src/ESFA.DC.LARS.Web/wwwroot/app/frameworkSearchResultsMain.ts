import Vue from "vue";
import store from "./store";
import { debounce } from 'vue-debounce';

import Filters from "./Components/filters.vue";
import FilterFeedback from './Components/filterFeedback.vue';
import { frameworkSearchService } from './Services/frameworkSearchService';
import { filterStoreService } from './Services/filterStoreService';
import { SearchType } from './SearchType';
import { ResultsHelper } from './Helpers/resultsHelper';
import { constants } from './constants';
import StorageService from './Services/storageService';
import LinkService from './Services/LinkService';

let vue = new Vue({
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

        const getDataAsync = async function () {
            const searchTerm: string = (<HTMLInputElement>document.getElementById("autocomplete-overlay"))?.value;
            return await frameworkSearchService.getResultsAsync(filterStoreService.getSavedFilters(SearchType.Frameworks), searchTerm);
        };

        const resultsHelper = new ResultsHelper(this.$refs["Results"] as HTMLElement, this.$refs["ResultsCount"] as HTMLElement, this.$refs["ValidationErrors"] as HTMLElement);

        const debouncedCallback = debounce(async () => { await resultsHelper.getResultsAsync(getDataAsync) }, constants.debounceTime);
        filterStoreService.watchFilters(SearchType.Frameworks, debouncedCallback, this.immediateRefresh, true);
    },
    methods: {
        setImmediateRefreshRequired: function (refreshRequired: boolean) {
            this.immediateRefresh = refreshRequired;
        },
        learningTypeChanged: function (value: string) {
            const storageService = new StorageService(sessionStorage);

            const linkService = new LinkService();
            if (value === SearchType.Frameworks) {
                window.location.href = linkService.getFrameworksSearchResultsLink();
            }
            if (value === SearchType.Units) {
                window.location.href = linkService.getUnitsSearchResultsLink();
            }
            if (value === SearchType.Qualifications) {
                window.location.href = linkService.getLearningAimSearchResultsLink();
            }
            storageService.clearFilters(constants.storageKey);
        }
    }
});