import Vue from "vue";
import store from "./store";

import Filters from "./Components/filters.vue";
import FilterFeedback from './Components/filterFeedback.vue';
import { standardSearchService } from './Services/standardSearchService';
import { FilterStoreService } from './Services/filterStoreService';
import { SearchType } from './Enums/SearchType';
import { ResultsHelper } from './Helpers/resultsHelper';
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
            immediateRefresh: false,
            linkService: new LinkService()
        };
    },
    mounted() {
        const getDataAsync = async function () {
            const filterStoreService = new FilterStoreService(SearchType.Standards);
            const searchTerm: string = (<HTMLInputElement>document.getElementById("autocomplete-overlay"))?.value;
            return await standardSearchService.getResultsAsync(filterStoreService.getSavedFilters(), searchTerm);
        };
        const resultsHelper = new ResultsHelper(this.$refs["Results"] as HTMLElement, this.$refs["ResultsCount"] as HTMLElement, this.$refs["ValidationErrors"] as HTMLElement);
        const needsClientRefresh = this.immediateRefresh || this.linkService.hasFilterQueryStringParam(window.location.search);
        resultsHelper.manageResults(getDataAsync, SearchType.Standards, needsClientRefresh);
    },
    methods: {
        setImmediateRefreshRequired: function (refreshRequired: boolean) {
            this.immediateRefresh = refreshRequired;
        },
        learningTypeChanged: function (value: string) {
            this.linkService.redirectToResults(value, SearchType.Standards);
        }
    }
});