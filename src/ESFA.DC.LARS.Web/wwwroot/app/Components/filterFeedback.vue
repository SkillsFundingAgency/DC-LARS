<template>
    <div class="filter-feedback-container" style="display: block;" v-if="filters.length > 0">
        <span id="from" class="govuk-body govuk-!-font-size-16">
            <strong>Showing </strong>
            <span id="firstFilter">
                <template v-for="filter in filters">
                    <a href="#" class="filter-feedback govuk-link" v-on:click="removeFilter(filter)">
                        <span class="filter-name">
                            <span class="close" v-bind:id="filter.key"></span>
                            <span v-html="filter.value"></span>
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
    import { FilterStoreService } from '../Services/filterStoreService';
    import { SearchType } from '../enums/SearchType';
    import { FilterType } from '../enums/FilterType';
    import StorageService from '../Services/storageService';
    import { constants } from '../constants';

    @Component
    export default class FilterFeedback extends Vue {
        @Prop() public searchType!: SearchType;
        private storageService: StorageService;
        private filterStoreService: FilterStoreService;

        get savedfilters(): Array<IFilterItem> {
            return this.filterStoreService.getSavedFilters();
        };

        private filters: Array<IFilterItem>;

        constructor() {
            super();
            this.filters = [];
            this.storageService = new StorageService(sessionStorage);
            this.filterStoreService = new FilterStoreService(this.searchType);
        }

        mounted() {
            this.filterStoreService.watchFilters(this.refreshFilters, true, true);
        }

        removeFilter(filter: IFilterItem): void {
            this.filters = filterService.removeFilterFromArray(this.savedfilters, filter);
            this.filterStoreService.updateStore(this.filters);
            this.storageService.updateFilters(constants.storageKey, this.filters);
        }

        private refreshFilters(): void {
            let filtersToSort = [...this.savedfilters]; 
            // Don't display teaching years as filter for now.  This could be reversed so if required just remove this code.
            filtersToSort = filtersToSort.filter(f => f.type !== FilterType.TeachingYears);
            this.filters = filterService.sortFilters(filtersToSort);
        }
    }
</script>