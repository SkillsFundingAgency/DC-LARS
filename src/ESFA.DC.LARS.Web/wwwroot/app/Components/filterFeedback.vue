<template>
    <div class="filter-feedback-container" style="display: block;" v-if="filters.length > 0">
        <span id="from" class="govuk-body govuk-!-font-size-16">
            <strong>Showing </strong>
            <span id="firstFilter">
                <template v-for="filter in filters">
                    <a href="#" class="filter-feedback" v-on:click="removeFilter(filter)">
                        <span class="filter-name">
                            <span class="close" v-bind:id="filter.key"></span>
                            {{ filter.value }}
                        </span>
                    </a>
                </template>
            </span>
        </span>
    </div>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';
    import { IFilterItem } from '../Interfaces/IFilterItem';
    import FilterService from '../Services/filterService';

    @Component
    export default class FilterFeedback extends Vue {

        get savedfilters(): Array<IFilterItem> {
            return this.$store.state.qualificationFilters;
        };

        private filters: Array<IFilterItem>;

        private filterService : FilterService;

        constructor() {
            super();
            this.filters = [];
            this.filterService = new FilterService();
        }

        mounted() {
            const classScope = this;
            this.$store.watch(
                function (state) {
                    return state.qualificationFilters;
                },
                function () {
                    classScope.refreshFilters();
                },
                {
                    immediate: true,
                    deep: true
                });
        }

        removeFilter(filter: IFilterItem): void {
            this.filters = this.filterService.removeFilterFromArray(this.savedfilters, filter);

            this.$store.commit('updateFilters', this.filters);
        }

        private refreshFilters() : void {
            //clone so we are not changing the store in the sort and triggering the watch ... recursion
            let filtersToSort  = [...this.savedfilters]; 

            this.filters = this.filterService.sortFilters(filtersToSort);
        }
    }
</script>