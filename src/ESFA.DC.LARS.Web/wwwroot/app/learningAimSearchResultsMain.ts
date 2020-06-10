import { Component } from 'vue-property-decorator';
import Filters from "../app/Components/filters.vue";
import FilterFeedback from '../app/Components/filterFeedback.vue';
import { SearchType } from './Enums/SearchType';
import { learningAimSearchService } from './Services/learningAimSearchService';
import AbstractSearchResultsComponent from './abstractSearchResultsComponent';

@Component({
    el: "#resultsApp",
    components: {
        'filter-feedback': FilterFeedback,
        'filters': Filters
    }
})
class ResultsApp extends AbstractSearchResultsComponent {
    searchType = SearchType.Qualifications;

    mounted() {
        this.intialise();
    }

    async getDataAsync() {
        const teachingYears: Array<string> = new Array(`${(<HTMLSelectElement>document.getElementById("TeachingYears"))?.value}`);
        return await learningAimSearchService.getQualificationsResultsAsync(this.savedFilters, this.searchTerm, teachingYears);
    };
}

const vue = new ResultsApp();