<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';
    import { IFilterItem, FilterType } from '../../app/Interfaces/IFilterItem';
    import { filterService } from '../Services/filterService';

    @Component({
        template: "#filtersTemplate"
    })
    export default class Filters extends Vue {
        private currentDisplayFilters: Array<IFilterItem> = [];

        mounted() {
            this.currentDisplayFilters = this.savedfilters;
            filterService.watchQualificationFilters(this, this.updateDisplay);
        }

        get savedfilters(): Array<IFilterItem> {
            return [...this.$store.state.qualificationFilters];
        };

        public updateCheckboxFilter(key: string, value: string, isChecked: boolean, type: FilterType): void {
            const filters = this.savedfilters; 
            const actionedFilter: IFilterItem = { key, value, type };

            isChecked ? filters.push(actionedFilter) : filterService.removeFilterFromArray(filters, actionedFilter);
            this.updateStore(filters);
        }

         public updateSelectFilter(key: string, value: string, type: FilterType): void {
            const filters = this.savedfilters; 
            const filter = filterService.findFilterItem(key, type, filters);

            filter ? filter.value = key : filters.push({key, value, type });
            this.updateStore(filters);
        }

        private updateStore(filters: Array<IFilterItem>) {
            this.$store.commit('updateFilters', filters);
            this.currentDisplayFilters = this.savedfilters;
        }

        private updateDisplay() {
            const addedFilters = this.savedfilters.filter(filter => this.currentDisplayFilters.indexOf(filter) < 0);
            const removedFilters = this.currentDisplayFilters.filter(filter => this.savedfilters.indexOf(filter) < 0);

            this.setFilterDisplay(addedFilters, true);
            this.setFilterDisplay(removedFilters, false);
        }

        private setFilterDisplay(filters: Array<IFilterItem> = [], isAdded: boolean): void {
            const classScope = this;

            //Assumption:  All filters of a certain type are located in the same container.
            Array.from(new Set(filters.map(filter => filter.type))).forEach(type => {
                const typeContainer = classScope.$refs[type.toString()] as HTMLElement;

                filters.filter(i => i.type === type).forEach(filter => {
                    !classScope.updateCheckboxDisplay(typeContainer, filter.key, isAdded)
                        && classScope.updateSelectDisplay(typeContainer, filter.key, isAdded);
                });
            });
        }

        private updateCheckboxDisplay(typeContainer :HTMLElement, key:string, isAdded: boolean) : boolean {
            const input = typeContainer.querySelector(`input[value='${key}']`) as HTMLInputElement;

            if (input && input.checked) {
                input.checked = isAdded;
                return true;
            }
            return false;
        }

        private updateSelectDisplay(typeContainer :HTMLElement, key:string, isAdded: boolean) : boolean {
            const select = typeContainer.querySelector("select") as HTMLSelectElement;

            if (select) {
                isAdded ? select.value = key : select.selectedIndex = 1; //TODO:  Not sure how this filter will work.
                return true;
            }
            return false;
        }
    }
</script>