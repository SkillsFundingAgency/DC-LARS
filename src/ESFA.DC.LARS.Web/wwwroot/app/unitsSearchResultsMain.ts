import { Component } from 'vue-property-decorator';
import Filters from "./Components/filters.vue";
import FilterFeedback from './Components/filterFeedback.vue';
import { SearchType } from './Enums/SearchType';
import AbstractSearchResultsComponent from './abstractSearchResultsComponent';
import { learningAimSearchService } from './Services/learningAimSearchService';
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
        const teachingYears: Array<string> = new Array(`${(<HTMLSelectElement>document.getElementById("TeachingYears"))?.value}`);
        return await learningAimSearchService.getUnitsResultsAsync(this.filterStoreService.getSavedFilters(), this.searchTerm, teachingYears);
    }

    getSearchType(): SearchType {
        return SearchType.Units;
    }
}
new ResultsApp().$mount('#resultsApp');

