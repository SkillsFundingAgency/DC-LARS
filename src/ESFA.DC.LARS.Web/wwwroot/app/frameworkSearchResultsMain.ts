import Vue from "vue";
import store from "./store";
import { debounce } from 'vue-debounce';

import Filters from "./Components/filters.vue";
import FilterFeedback from './Components/filterFeedback.vue';
import { frameworkSearchService } from './Services/frameworkSearchService';
import { filterStoreService } from './Services/filterStoreService';
import { SearchType } from './Enums/SearchType';
import { ResultsHelper } from './Helpers/resultsHelper';
import { constants } from './constants';
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
            const linkService = new LinkService();
            linkService.redirectToResults(value, SearchType.Frameworks);
        }
    }
});