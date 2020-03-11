import Vue from "vue";
import Search from "../app/search.vue";

let vue = new Vue({
    el: "#app",
    components: {
        'search': Search
    }
});