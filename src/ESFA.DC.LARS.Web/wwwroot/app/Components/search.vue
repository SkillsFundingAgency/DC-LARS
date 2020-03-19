<template>
    <portal to="filter-feedback">
        <filter-feedback></filter-feedback>
    </portal>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';
    import FilterFeedback from './filterFeedback.vue';

    @Component({
      components: {
        'filter-feedback' : FilterFeedback
      }
    })
    export default class Search extends Vue {
    }
</script>