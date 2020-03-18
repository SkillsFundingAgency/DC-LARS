<template>
    <portal to="filter-feedback">
        <filter-feedback></filter-feedback>
    </portal>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';
    import FilterFeedback from '../app/FilterFeedback/filterFeedback.vue';
    import { FilterType, IFilterItem } from '../app/Interfaces/IFilterItem';

    @Component({
      components: {
        'filter-feedback' : FilterFeedback
      }
    })
    export default class Search extends Vue {
        private filters: Array<IFilterItem>;

        get savedfilters(): Array<IFilterItem> {
            return this.$store.state.filters;
        };

        constructor() {
            super();
            this.filters = this.$store.state.filters;
        }
        
        mounted() {
            this.init();
        }

        private init() : void {
            //example state
            this.filters.push({ key : 'foo', value : 'bar', type: FilterType.AwardingBody});
            this.$store.commit('updateFilters', this.filters);
        }
    }
</script>