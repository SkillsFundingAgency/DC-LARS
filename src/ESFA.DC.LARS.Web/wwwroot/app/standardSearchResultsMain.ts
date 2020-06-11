import { Component } from 'vue-property-decorator';
import Filters from "./Components/filters.vue";
import FilterFeedback from './Components/filterFeedback.vue';
import { standardSearchService } from './Services/standardSearchService';
import { SearchType } from './Enums/SearchType';
import AbstractSearchResultsComponent from './abstractSearchResultsComponent';

@Component({
    el: "#resultsApp",
    components: {
        'filter-feedback': FilterFeedback,
        'filters': Filters
    }
})
class ResultsApp extends AbstractSearchResultsComponent {

    mounted() {
        this.intialise();
    }

    async getDataAsync() {
        return await standardSearchService.getResultsAsync(this.filterStoreService.getSavedFilters(), this.searchTerm);
    };

    getSearchType(): SearchType {
        return SearchType.Standards
    }
}

const vue = new ResultsApp();
