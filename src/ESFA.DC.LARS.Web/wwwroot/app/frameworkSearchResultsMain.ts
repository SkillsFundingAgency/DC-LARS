import { Component } from 'vue-property-decorator';
import Filters from "./Components/filters.vue";
import FilterFeedback from './Components/filterFeedback.vue';
import { frameworkSearchService } from './Services/frameworkSearchService';
import { SearchType } from './Enums/SearchType';
import AbstractSearchResultsComponent from "./abstractSearchResultsComponent";

@Component({
    el: "#resultsApp",
    components: {
        'filter-feedback': FilterFeedback,
        'filters': Filters
    }
})
class ResultsApp extends AbstractSearchResultsComponent {
    searchType = SearchType.Frameworks;

    mounted() {
        this.intialise();
    }

    async getDataAsync() {
        return await frameworkSearchService.getResultsAsync(this.savedFilters, this.searchTerm);
    };
}

const vue = new ResultsApp();
