﻿import Vue from "vue";
import Search from "../app/Components/search.vue";

import store from "../app/store"

import Filters from "../app/Components/filters.vue";
import PortalVue from 'portal-vue';

Vue.use(PortalVue);

let vue = new Vue({
    el: "#app",
    store,
    components: {
        'search': Search,
        'filters': Filters
    }
});