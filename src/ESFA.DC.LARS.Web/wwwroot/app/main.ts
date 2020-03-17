import Vue from "vue";
import Vuex from "vuex";
import Search from "../app/search.vue";

import store from "../app/store"

import PortalVue from 'portal-vue';

Vue.use(Vuex);
Vue.use(PortalVue);

let vue = new Vue({
    el: "#app",
    store,
    components: {
        'search' : Search
    }
});