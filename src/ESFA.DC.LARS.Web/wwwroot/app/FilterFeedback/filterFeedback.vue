<template>
    <div class="filter-feedback-container" style="display: block;">
        <span id="from" class="govuk-body govuk-!-font-size-16">
            <strong>Showing </strong>
            <span id="firstFilter">
                <template v-for="filter in filters">
                    <a href="#" class="filter-feedback">
                        <span class="filter-name">
                            <span class="close"></span>
                            {{ filter }}
                        </span>
                    </a>
                </template>
            </span>
        </span>
    </div>
</template>

<script lang="ts">
    import Vue from "../../assets/dist/minified/vue.min.js";
    import Component from "../../assets/dist/minified/vue-class-component.min.js";
    import Prop from '../../assets/dist/js/vue-property-decorator.js';

    interface SearchFilters {
        levels : Array<String>
    }

    @Component
    export default class FilterFeedback extends Vue {
        @Prop() readonly searchFilters: SearchFilters;

        private  filters: Array<String>;

        mounted() {
            this.init();
        }

        private init() : void {
                //alert('props: ' + this.searchFilters);

                for (let filter of this.searchFilters.levels) {
                    //alert(filter);
                    this.filters.push(filter);
                }
            }
    }
</script>