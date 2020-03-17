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
    import { ISearchFilters } from '../../app/Interfaces/ISearchFilters';
    import { IFilterItem } from '../../app/Interfaces/IFilterItem';

    @Component
    export default class FilterFeedback extends Vue {

        get savedfilters(): ISearchFilters {
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
            const prefix = filter.key.substring(0, 2);
            filter.key = filter.key.substring(3);

            let filters = this.savedfilters;
            switch (prefix) {
                case 'ab':
                    filters.awardingBodies = this.removeFromArray(filters.awardingBodies, filter);
                    break;
                case 'lv':
                    filters.levels = this.removeFromArray(filters.levels, filter);
                    break;
                case 'fs':
                    filters.fundingStreams = this.removeFromArray(filters.fundingStreams, filter);
                    break;
                case 'ty':
                    filters.teachingYears = this.removeFromArray(filters.teachingYears, filter);
                    break;
            }

            this.$store.commit('updateFilters', filters);
        }

        private removeFromArray(filters: Array<IFilterItem>, item: IFilterItem) : Array<IFilterItem> {
            const found = filters.find(function (filter) {
                return filter.key === item.key;
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
            const classScope = this;
            this.filters = [];
            
            if (this.savedfilters.awardingBodies !== undefined) {
                this.savedfilters.awardingBodies.forEach(function (value) {
                    classScope.filters.push({key: 'ab-' + value.key, value: value.value });
                });
            }

            if (this.savedfilters.levels !== undefined) {
                this.savedfilters.levels.forEach(function (value) {
                    classScope.filters.push({key: 'lv-' + value.key, value: value.value });
                });
            }

            if (this.savedfilters.fundingStreams !== undefined) {
                this.savedfilters.fundingStreams.forEach(function (value) {
                    classScope.filters.push({key: 'fs-' + value.key, value: value.value });
                });
            }

            if (this.savedfilters.teachingYears !== undefined) {
                this.savedfilters.teachingYears.forEach(function (value) {
                    classScope.filters.push({key: 'ty-' + value.key, value: value.value });
                });
            }
        }
    }
</script>