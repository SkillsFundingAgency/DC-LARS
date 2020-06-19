import { Component } from 'vue-property-decorator';
import Filters from "./Components/filters.vue";
import FilterFeedback from './Components/filterFeedback.vue';
import { standardSearchService } from './Services/standardSearchService';
import { SearchType } from './Enums/SearchType';
import AbstractSearchResultsComponent from './abstractSearchResultsComponent';
import { ISearchResults } from './Interfaces/ISearchResults';

@Component({
    components: {
        'filter-feedback': FilterFeedback,
        'filters': Filters
    }
})
class ResultsApp extends AbstractSearchResultsComponent {

    mounted(): void {
        this.intialise();
    }

    async getDataAsync(): Promise<ISearchResults> {
        return await standardSearchService.getResultsAsync(this.filterStoreService.getSavedFilters(), this.searchTerm);
    }

    getSearchType(): SearchType {
        return SearchType.Standards
    }
}

new ResultsApp().$mount('#resultsApp');
