<template v-slot:feedback>
    <filter-feedback v-bind:search-filters='filters'></filter-feedback>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';
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
            this.filters = {};
        }
    }
</script>