import Vue from "vue";
import store from "./store"
import { debounce } from 'vue-debounce';

import Filters from "../app/Components/filters.vue";
import FilterFeedback from '../app/Components/filterFeedback.vue';
import { filterStoreService } from './Services/filterStoreService';
import { SearchType } from './SearchType';
import { qualificationSearchService } from './Services/qualificationSearchService';
import { ResultsDisplayHelper } from './Helpers/resultsDisplayHelper';

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
        const callback = async function () {
            const resultsContainer = classScope.$refs["Results"] as HTMLElement;
            if (resultsContainer) {
                const displayHelper = new ResultsDisplayHelper(resultsContainer, classScope.$refs["ResultsCount"] as HTMLElement, classScope.$refs["ValidationErrors"] as HTMLElement);
                displayHelper.setIsLoading();

                const searchTerm: string = (<HTMLInputElement>document.getElementById("autocomplete-overlay"))?.value;
                const teachingYears: Array<string> = new Array(`${(<HTMLSelectElement>document.getElementById("TeachingYears"))?.value}`);

                var response = await qualificationSearchService.getResultsAsync(classScope.$store.state.qualificationFilters, searchTerm, teachingYears);

                displayHelper.updateForResponse(response);
            }
        }

        const debouncedCallback = debounce(callback, '600ms');
        filterStoreService.watchFilters(SearchType.Qualifications, debouncedCallback, this.immediateRefresh, true);
    },
    methods: {
        setImmediateRefreshRequired: function (refreshRequired: boolean) {
            this.immediateRefresh = refreshRequired;
        }
    }
});