import { Component } from 'vue-property-decorator';
import Filters from "./Components/filters.vue";
import FilterFeedback from './Components/filterFeedback.vue';
import { SearchType } from './Enums/SearchType';
import AbstractSearchResultsComponent from './abstractSearchResultsComponent';
import { learningAimSearchService } from './Services/learningAimSearchService';

@Component({
    el: "#resultsApp",
    components: {
        'filter-feedback': FilterFeedback,
        'filters': Filters
    }
})
class ResultsApp extends AbstractSearchResultsComponent {
    searchType = SearchType.Units;

    mounted() {
        this.intialise();
    }

    async getDataAsync() {
        const teachingYears: Array<string> = new Array(`${(<HTMLSelectElement>document.getElementById("TeachingYears"))?.value}`);
        return await learningAimSearchService.getUnitsResultsAsync(this.savedFilters, this.searchTerm, teachingYears);
    };
}

const vue = new ResultsApp();

