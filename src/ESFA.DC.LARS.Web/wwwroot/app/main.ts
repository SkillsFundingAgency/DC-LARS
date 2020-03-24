import Vue from "vue";
import store from "../app/store"
import PortalVue from 'portal-vue';

import Filters from "../app/Components/filters.vue";
import FilterFeedback from '../app/Components/filterFeedback.vue';
import { resultsService } from '../app/Services/resultsService';
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
            const resultsContainer = classScope.$refs["Results"] as HTMLElement;
            const resultCount = classScope.$refs["ResultsCount"] as HTMLElement;
            if (resultsContainer && resultCount) {
                resultsContainer.innerHTML = "Loading";
                var response = await resultsService.getResultsAsync(classScope.$store.state.qualificationFilters);
                resultsContainer.innerHTML = response.data;
                resultCount.innerHTML = `${response.count} results`;
            }
        }

        filterService.watchQualificationFilters(this, callback, false, true);
    }
});