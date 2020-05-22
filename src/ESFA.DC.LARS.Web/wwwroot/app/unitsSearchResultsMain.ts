import Vue from "vue";
import store from "./store"
import Filters from "../app/Components/filters.vue";
import FilterFeedback from '../app/Components/filterFeedback.vue';
import { filterStoreService } from './Services/filterStoreService';
import { SearchType } from './Enums/SearchType';
import { learningAimSearchService } from './Services/learningAimSearchService';
import { ResultsHelper } from './Helpers/resultsHelper';
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
            immediateRefresh: false,
            linkService: new LinkService()
        };
    },
    mounted() {
        const getDataAsync = async function () {
            const searchTerm: string = (<HTMLInputElement>document.getElementById("autocomplete-overlay"))?.value;
            const teachingYears: Array<string> = new Array(`${(<HTMLSelectElement>document.getElementById("TeachingYears"))?.value}`);
            return await learningAimSearchService.getUnitsResultsAsync(filterStoreService.getSavedFilters(SearchType.Units), searchTerm, teachingYears);
        }
        
        const resultsHelper = new ResultsHelper(this.$refs["Results"] as HTMLElement, this.$refs["ResultsCount"] as HTMLElement, this.$refs["ValidationErrors"] as HTMLElement);
        const needsClientRefresh = this.immediateRefresh || this.linkService.hasFilterQueryStringParam(window.location.search);
        resultsHelper.manageResults(getDataAsync, SearchType.Units, needsClientRefresh);
    },
    methods: {
        setImmediateRefreshRequired: function (refreshRequired: boolean) {
            this.immediateRefresh = refreshRequired;
        },
        learningTypeChanged: function (value: string) {
            this.linkService.redirectToResults(value, SearchType.Units);
        }
    }
});