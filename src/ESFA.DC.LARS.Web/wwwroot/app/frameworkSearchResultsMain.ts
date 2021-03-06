﻿import { Component } from 'vue-property-decorator';
import Filters from "./Components/filters.vue";
import FilterFeedback from './Components/filterFeedback.vue';
import { frameworkSearchService } from './Services/frameworkSearchService';
import { SearchType } from './Enums/SearchType';
import AbstractSearchResultsComponent from "./abstractSearchResultsComponent";
import { ISearchResults } from './Interfaces/ISearchResults';

@Component({
    components: {
        'filter-feedback': FilterFeedback,
        'filters': Filters
    }
})
class FrameworkSearchResults extends AbstractSearchResultsComponent {

    mounted(): void {
        this.intialise();
    }

    async getDataAsync(): Promise<ISearchResults> {
        return await frameworkSearchService.getResultsAsync(this.filterStoreService.getSavedFilters(), this.searchTerm);
    }

    getSearchType(): SearchType {
        return SearchType.Frameworks
    }
}

new FrameworkSearchResults().$mount('#resultsApp');
