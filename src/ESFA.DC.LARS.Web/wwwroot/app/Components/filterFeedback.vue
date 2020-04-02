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
    import { Component, Vue, Prop } from 'vue-property-decorator';
    import { IFilterItem } from '../Interfaces/IFilterItem';
    import { filterService } from '../Services/filterService';
    import { SearchType } from '../SearchType';

    @Component
    export default class FilterFeedback extends Vue {
         @Prop() public searchType!: SearchType;

        get savedfilters(): Array<IFilterItem> {
            return filterService.savedFilters(this, this.searchType);
        };

        private filters: Array<IFilterItem>;

        constructor() {
            super();
            this.filters = [];
        }

        mounted() {
            filterService.watchFilters(this, this.searchType, this.refreshFilters, true, true);
        }

        removeFilter(filter: IFilterItem): void {
            this.filters = filterService.removeFilterFromArray(this.savedfilters, filter);
            filterService.updateStore(this, this.searchType, this.filters);
        }

        private refreshFilters(): void {
            let filtersToSort  = [...this.savedfilters]; 
            console.dir(filtersToSort);

            this.filters = filterService.sortFilters(filtersToSort);
        }
    }
</script>