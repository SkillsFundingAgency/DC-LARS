import Vue from "vue";
import store from "../app/store"
import PortalVue from 'portal-vue';

import Filters from "../app/Components/filters.vue";
import FilterFeedback from '../app/Components/filterFeedback.vue';
import { searchService } from './Services/searchService';
import { filterService } from '../app/Services/filterService';

Vue.use(PortalVue);

let vue = new Vue({
    el: "#app",
    store,
    components: {
        'filter-feedback': FilterFeedback,
        'filters': Filters
    },
    mounted() {
        const classScope = this;
        
        const callback = async function () {
            const resultsContainer = <HTMLElement>classScope.$refs["Results"];
            const resultCount = <HTMLElement>classScope.$refs["ResultsCount"];
            const validationErrorContainer = <HTMLElement>classScope.$refs["ValidationErrors"];

            resultsContainer.innerHTML = "Loading";

            var response = await searchService.getResultsAsync(classScope.$store.state.qualificationFilters);

            resultsContainer.innerHTML = response.data;
            resultCount.innerHTML = `${response.count} results`;
            validationErrorContainer.innerHTML = response.validationErrors.map(v => `<li class="govuk-error-message">${v}</li>`).join();
        }

        filterService.watchQualificationFilters(this, callback, false, true);
    }
});