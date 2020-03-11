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
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import { ISearchFilters } from '../../app/Interfaces/ISearchFilters';


    @Component
    export default class FilterFeedback extends Vue {
        @Prop() readonly searchFilters!: ISearchFilters;

        private filters: Map<string, string>;

        constructor() {
            super();
            this.filters = new Map();
        }

        mounted() {
            this.init();
        }

        private init() : void {
            const classScope = this;

            if (this.searchFilters.awardingBodies !== undefined) {
                this.searchFilters.awardingBodies.forEach(function (value, key) {
                    classScope.filters.set(key, value);
                });
            }

            if (this.searchFilters.levels !== undefined) {
                this.searchFilters.levels.forEach(function (value, key) {
                    classScope.filters.set(key, value);
                });
            }

            if (this.searchFilters.fundingStreams !== undefined) {
                this.searchFilters.fundingStreams.forEach(function (value, key) {
                    classScope.filters.set(key, value);
                });
            }

            if (this.searchFilters.teachingYears !== undefined) {
                this.searchFilters.teachingYears.forEach(function (value, key) {
                    classScope.filters.set(key, value);
                });
            }
        }
    }
</script>