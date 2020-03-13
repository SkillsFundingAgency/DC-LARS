<template v-slot="feedback">
    <filter-feedback v-bind:search-filters='filters'></filter-feedback>
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
                flag : "",
                awardingBodies: [],
                levels : [],
                fundingStreams : [],
                teachingYears : []
            };
        }
        
        mounted() {
            this.init();
        }

        private init() : void {
            this.filters.awardingBodies.push({ key : 'foo', value : 'bar' });
            Vue.set(this.filters.awardingBodies, 'foo', 'bar');

            this.filters.flag = "changed";

            let index = this.filters.awardingBodies.findIndex(x => x.key === 'foo');
            alert('Setting length: ' + this.filters.awardingBodies.length);
            alert('Setting value: ' + this.filters.awardingBodies[index]);
        }
    }
</script>