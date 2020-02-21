import Vue from "../assets/dist/minified/vue.min.js";
import FilterFeedback from "./FilterFeedback/filterFeedback";

var app = new Vue({
    el: "#app",
    components: {
        'filter-feedback': FilterFeedback
    }
});