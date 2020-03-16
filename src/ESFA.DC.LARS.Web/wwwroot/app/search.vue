<template>
    <portal to="filter-feedback">
        <filter-feedback v-bind:search-filters='filters'></filter-feedback>
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

        constructor() {
            super();
            this.filters = {
                awardingBodies: [],
                levels : [],
                fundingStreams : [],
                teachingYears : []
            };

            alert('search constructor');
        }
        
        mounted() {
            this.init();
        }

        private init() : void {
            this.filters.awardingBodies.push({ key : 'foo', value : 'bar' });
            Vue.set(this.filters.awardingBodies, 'foo', 'bar');
        }
    }
</script>