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
    
    mounted() {
        this.intialise();
    }

    async getDataAsync() {
        const teachingYears: Array<string> = new Array(`${(<HTMLSelectElement>document.getElementById("TeachingYears"))?.value}`);
        return await learningAimSearchService.getQualificationsResultsAsync(this.filterStoreService.getSavedFilters(), this.searchTerm, teachingYears);
    };

    getSearchType(): SearchType {
        return SearchType.Qualifications
    }
}

const vue = new ResultsApp();