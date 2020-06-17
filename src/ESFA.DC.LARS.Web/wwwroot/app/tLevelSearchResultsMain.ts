import { Component } from 'vue-property-decorator';
import Filters from "./Components/filters.vue";
import FilterFeedback from './Components/filterFeedback.vue';
import { SearchType } from './Enums/SearchType';
import AbstractSearchResultsComponent from './abstractSearchResultsComponent';
import { tlevelSearchService } from './Services/tlevelSearchService';

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
        return await tlevelSearchService.getResultsAsync(this.filterStoreService.getSavedFilters(), this.searchTerm);
    }

    getSearchType(): SearchType {
        return SearchType.TLevels;
    }
}

new ResultsApp();

