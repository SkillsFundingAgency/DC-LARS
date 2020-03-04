<template>
    <div class="filter-feedback-container" style="display: block;">
        <span id="from" class="govuk-body govuk-!-font-size-16">
            <strong>Showing </strong>
            <span id="firstFilter">
                <template v-for="filter in filters">
                    <a href="#" class="filter-feedback">
                        <span class="filter-name">
                            <span class="close"></span>
                            {{ filter }}
                        </span>
                    </a>
                </template>
            </span>
        </span>
    </div>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';

    interface SearchFilters {
        levels : Array<String>
    }

    @Component
    export default class FilterFeedback extends Vue {
        @Prop() readonly searchFilters!: SearchFilters;

        private filters: Array<String>;

        constructor() {
            super();
            this.filters = [];
        }

        mounted() {
            this.init();
        }

        private init() : void {
                //alert('props: ' + this.searchFilters);

                for (let filter of this.searchFilters.levels) {
                    //alert(filter);
                    this.filters.push(filter);
                }
            }
    }
</script>