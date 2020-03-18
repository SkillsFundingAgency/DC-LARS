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
    import { IFilterItem } from '../../app/Interfaces/IFilterItem';

    @Component
    export default class FilterFeedback extends Vue {

        get savedfilters(): Array<IFilterItem> {
            return this.$store.state.filters;
        };

        private filters: Array<IFilterItem>;

        constructor() {
            super();
            this.filters = [];
        }

        mounted() {
            const classScope = this;
            this.$store.watch(
                function (state) {
                    return state.filters;
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
            this.filters = this.removeFromArray(this.savedfilters, filter);

            this.$store.commit('updateFilters', this.filters);
        }

        private removeFromArray(filters: Array<IFilterItem>, item: IFilterItem) : Array<IFilterItem> {
            const found = filters.find(function (filter) {
                return filter.key === item.key && filter.type === item.type;
            });

            if (found !== undefined) {
                const index = filters.indexOf(found);
                if (index > -1) {
                    filters.splice(index, 1);
                }
            }
            return filters;
        }

        private refreshFilters() : void {
            //clone so we are not changing the store in the sort and triggering the watch ... recursion
            let filtersToSort  = [...this.savedfilters]; 

            this.filters = this.sortFilters(filtersToSort);
        }

        private sortFilters(filters: Array<IFilterItem>): Array<IFilterItem> {
            return filters.sort((f1, f2) => {
                if (f1.type < f2.type) {
                    return 1;
                }

                if (f1.type > f2.type) {
                    return -1;
                }

                if (f1.value > f2.value) {
                    return 1;
                }

                if (f1.value < f2.value) {
                    return -1;
                }

                return 0;
            });
        }
    }
</script>