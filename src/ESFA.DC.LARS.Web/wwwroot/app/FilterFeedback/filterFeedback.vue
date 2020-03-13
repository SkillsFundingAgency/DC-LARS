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
    import { Watch, Component, Prop, Vue } from 'vue-property-decorator';
    import { ISearchFilters } from '../../app/Interfaces/ISearchFilters';
    import { IFilterItem } from '../../app/Interfaces/IFilterItem';

    @Component
    export default class FilterFeedback extends Vue {
        @Prop() readonly searchFilters!: ISearchFilters;

        @Watch('searchFilters', { immediate: true, deep: true })
            filtersChanged() {
              this.refreshFilters();
            };

        private filters: Array<IFilterItem>;

        constructor() {
            super();
            this.filters = [];
        }

        private refreshFilters() : void {
            const classScope = this;

            if (this.searchFilters.awardingBodies !== undefined) {
                this.searchFilters.awardingBodies.forEach(function (value) {
                    classScope.filters.push({key: value.key, value: value.value });
                });
            }

            if (this.searchFilters.levels !== undefined) {
                this.searchFilters.levels.forEach(function (value) {
                    classScope.filters.push({key: value.key, value: value.value });
                });
            }

            if (this.searchFilters.fundingStreams !== undefined) {
                this.searchFilters.fundingStreams.forEach(function (value) {
                    classScope.filters.push({key: value.key, value: value.value });
                });
            }

            if (this.searchFilters.teachingYears !== undefined) {
                this.searchFilters.teachingYears.forEach(function (value) {
                    classScope.filters.push({key: value.key, value: value.value });
                });
            }
        }
    }
</script>