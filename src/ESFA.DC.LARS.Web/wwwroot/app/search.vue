<template>
    <portal to="filter-feedback">
        <filter-feedback></filter-feedback>
    </portal>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';
    import FilterFeedback from '../app/FilterFeedback/filterFeedback.vue';
    import { ISearchFilters } from '../app/Interfaces/ISearchFilters';
    
    @Component({
      components: {
        'filter-feedback' : FilterFeedback
      }
    })
    export default class Search extends Vue {
        private filters: ISearchFilters;

        get savedfilters(): ISearchFilters {
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
            //this.filters.awardingBodies.push({ key : 'foo', value : 'bar' });
            //Vue.set(this.filters.awardingBodies, 'foo', 'bar');
            //this.$store.commit('updateFilters', this.filters);
        }
    }
</script>