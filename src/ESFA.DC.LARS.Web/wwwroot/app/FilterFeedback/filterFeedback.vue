<template>
    <div class="filter-feedback-container" style="display: block;" v-if="filters.length > 0">
        <span id="from" class="govuk-body govuk-!-font-size-16">
            <strong>Showing </strong>
            <span id="firstFilter">
                <template v-for="(value, key) in filters">
                    <a href="#" class="filter-feedback">
                        <span class="filter-name">
                            <span class="close" v-bind:id="key"></span>
                            {{ value }}
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

        @Watch('searchFilters')
            filtersChanged(newFilters: ISearchFilters) {
              this.refreshFilters(newFilters);
            };

        private filters: Array<IFilterItem>;

        constructor() {
            super();
            this.filters = [];
        }

        private refreshFilters(newFilters: ISearchFilters) : void {
            const classScope = this;

            alert('in filter refreshFilters');
            let index = this.searchFilters.awardingBodies.findIndex(x => x.key === 'foo');
            alert('filter: ' + newFilters.awardingBodies[index]);

            if (this.searchFilters.awardingBodies !== undefined) {
                this.searchFilters.awardingBodies.forEach(function (value) {
                    alert('add ' + value.key + 'to awardingbodies');
                    classScope.filters.push({key: value.key, value: value.value });
                });
            }

            if (this.searchFilters.levels !== undefined) {
                this.searchFilters.levels.forEach(function (value) {
                    alert('add ' + value.key + 'to levels');
                    classScope.filters.push({key: value.key, value: value.value });
                });
            }

            if (this.searchFilters.fundingStreams !== undefined) {
                this.searchFilters.fundingStreams.forEach(function (value) {
                    alert('add ' + value.key + 'to fundingstreams');
                    classScope.filters.push({key: value.key, value: value.value });
                });
            }

            if (this.searchFilters.teachingYears !== undefined) {
                this.searchFilters.teachingYears.forEach(function (value) {
                    alert('add ' + value.key + 'to teachingyears');
                    classScope.filters.push({key: value.key, value: value.value });
                });
            }
        }
    }
</script>