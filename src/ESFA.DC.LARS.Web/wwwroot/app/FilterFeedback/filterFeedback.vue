<template>
    <div class="filter-feedback-container" style="display: block;">
        <span id="from" class="govuk-body govuk-!-font-size-16">
            <strong>Showing </strong>
            <span id="firstFilter">
                <template v-for="filter in filters">
                    <a href="#" class="filter-feedback">
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

        private refreshFilters() : void {
            const classScope = this;

            if (this.savedfilters.awardingBodies !== undefined) {
                this.savedfilters.awardingBodies.forEach(function (value) {
                    classScope.filters.push({key: value.key, value: value.value });
                });
            }

            if (this.savedfilters.levels !== undefined) {
                this.savedfilters.levels.forEach(function (value) {
                    classScope.filters.push({key: value.key, value: value.value });
                });
            }

            if (this.savedfilters.fundingStreams !== undefined) {
                this.savedfilters.fundingStreams.forEach(function (value) {
                    classScope.filters.push({key: value.key, value: value.value });
                });
            }

            if (this.savedfilters.teachingYears !== undefined) {
                this.savedfilters.teachingYears.forEach(function (value) {
                    classScope.filters.push({key: value.key, value: value.value });
                });
            }
        }
    }
</script>