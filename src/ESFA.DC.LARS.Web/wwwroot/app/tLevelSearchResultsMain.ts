import { Component } from 'vue-property-decorator';
import Filters from "./Components/filters.vue";
import FilterFeedback from './Components/filterFeedback.vue';
import { SearchType } from './Enums/SearchType';
import AbstractSearchResultsComponent from './abstractSearchResultsComponent';
import { tlevelSearchService } from './Services/tlevelSearchService';
import { ISearchResults } from './Interfaces/ISearchResults';

@Component({
    components: {
        'filter-feedback': FilterFeedback,
        'filters': Filters
    }
})
class TLevelSearchResults extends AbstractSearchResultsComponent {

    mounted(): void {
        this.intialise();
    }

    async getDataAsync(): Promise<ISearchResults> {
        return await tlevelSearchService.getResultsAsync(this.filterStoreService.getSavedFilters(), this.searchTerm);
    }

    getSearchType(): SearchType {
        return SearchType.TLevels;
    }
}

new TLevelSearchResults().$mount('#resultsApp');

