import Vue from "vue";
import Search from "../app/search.vue";

import store from "../app/store"

import PortalVue from 'portal-vue';

Vue.use(PortalVue);

let vue = new Vue({
    el: "#app",
    store,
    components: {
        'search' : Search
    }
});