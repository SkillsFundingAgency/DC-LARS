import Vue from "vue";
import FilterFeedback from "../app/FilterFeedback/filterFeedback.vue";

var app = new Vue({
    el: "#app",
    components: {
        'filter-feedback': FilterFeedback
    }
});