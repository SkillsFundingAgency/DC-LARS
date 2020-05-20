import Vue from "vue";
import store from "./store"
import { debounce } from 'vue-debounce';

import Filters from "../app/Components/filters.vue";
import FilterFeedback from '../app/Components/filterFeedback.vue';
import { filterStoreService } from './Services/filterStoreService';
import { SearchType } from './Enums/SearchType';
import { learningAimSearchService } from './Services/learningAimSearchService';
import { ResultsHelper } from './Helpers/resultsHelper';
import { constants } from './constants';
import LinkService from './Services/LinkService';

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
        const getDataAsync = async function () {
            const searchTerm: string = (<HTMLInputElement>document.getElementById("autocomplete-overlay"))?.value;
            const teachingYears: Array<string> = new Array(`${(<HTMLSelectElement>document.getElementById("TeachingYears"))?.value}`);
            return await learningAimSearchService.getQualificationsResultsAsync(filterStoreService.getSavedFilters(SearchType.Qualifications), searchTerm, teachingYears);
        }
        
        const resultsHelper = new ResultsHelper(this.$refs["Results"] as HTMLElement, this.$refs["ResultsCount"] as HTMLElement, this.$refs["ValidationErrors"] as HTMLElement);

        const debouncedCallback = debounce(async () => { await resultsHelper.getResultsAsync(getDataAsync) }, constants.debounceTime);
        filterStoreService.watchFilters(SearchType.Qualifications, debouncedCallback, this.immediateRefresh, true);
    },
    methods: {
        setImmediateRefreshRequired: function (refreshRequired: boolean) {
            this.immediateRefresh = refreshRequired;
        },
        learningTypeChanged: function (value: string) {
            const linkService = new LinkService();
            linkService.redirectToResults(value, SearchType.Qualifications);
        }
    }
});