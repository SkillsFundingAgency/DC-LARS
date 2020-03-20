import Vue from "vue";
import store from "../app/store"
import PortalVue from 'portal-vue';

import Filters from "../app/Components/filters.vue";
import FilterFeedback from '../app/Components/filterFeedback.vue';

Vue.use(PortalVue);

let vue = new Vue({
    el: "#app",
    store,
    components: {
        'filter-feedback': FilterFeedback,
        'filters': Filters
    }
});