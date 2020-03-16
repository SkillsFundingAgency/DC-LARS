import Vue from "vue";
import Search from "../app/search.vue";

import PortalVue from 'portal-vue';

Vue.use(PortalVue);

let vue = new Vue({
    el: "#app",
    components: {
        'search' : Search
    }
});