﻿import Vue from "../assets/dist/minified/vue.min.js";
import FilterFeedback from "../app/FilterFeedback/filterFeedback.vue";

var app = new Vue({
    el: "#app",
    components: {
        'filter-feedback': FilterFeedback
    }
});