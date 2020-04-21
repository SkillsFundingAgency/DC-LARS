<script lang="ts">
    import { Component, Vue, Prop } from 'vue-property-decorator';
    import { IFilterItem, FilterType } from '../../app/Interfaces/IFilterItem';
    import { filterService } from '../Services/filterService';
    import { filterStoreService } from '../Services/filterStoreService';
    import { accordionService } from '../Services/accordionService';
    import { SearchType } from '../SearchType';
    import StorageService from '../Services/storageService';
    import FilterHistoryService from '../Services/filterHistoryService';
    import { constants } from '../constants';

    @Component({
        template: "#filtersTemplate"
    })
    export default class Filters extends Vue {
        @Prop() public searchType!: SearchType;
        @Prop() public setImmediateRefreshRequired!: Function;

        private currentDisplayFilters: Array<IFilterItem> = [];
        private storageService: StorageService;
        private filterHistoryService: FilterHistoryService;

        constructor() {
            super();
            this.filterHistoryService = new FilterHistoryService(this.searchType);
            this.storageService = new StorageService(sessionStorage);
        }

        mounted() {
            accordionService.initialiseAccordion();
            this.getFilterHistory();
            this.currentDisplayFilters = this.savedfilters;
            filterStoreService.watchFilters(this.searchType, () => this.updateDisplay(this.savedfilters, this.currentDisplayFilters), false, true);
        }

        get savedfilters(): Array<IFilterItem> {
            return [...filterStoreService.getSavedFilters(this.searchType)];
        };

        public clearFilters(): void {
            filterStoreService.updateStore(this.searchType, []);
            this.storageService.clearFilters(constants.storageKey);
            this.updateDisplay(this.savedfilters, this.currentDisplayFilters);
            this.currentDisplayFilters = [];
        }

        public updateCheckboxFilter(key: string, value: string, isChecked: boolean, type: FilterType): void {
            const filters = this.savedfilters;
            const actionedFilter: IFilterItem = { key, value, type };

            isChecked ? filters.push(actionedFilter) : filterService.removeFilterFromArray(filters, actionedFilter);

            this.storageService.updateFilters(constants.storageKey, filters);
            this.updateStore(filters);
        }

        public updateSelectFilter(key: string, value: string, type: FilterType): void {
            const filters = this.savedfilters;
            let filter = filterService.findFilterItemByType(type, filters);

            filter ? filter = Object.assign(filter, { key: key, value: value }) : filters.push({ key, value, type });

            this.storageService.updateFilters(constants.storageKey, filters);
            this.updateStore(filters);
        }

        public updateAccordion(id: string): void {
            accordionService.toggleSection(id, false);
        }

        public updateAccordionAll(): void {
            accordionService.updateAccordionAll();
        }

        private updateStore(filters: Array<IFilterItem>) {
            filterStoreService.updateStore(this.searchType, filters);
            this.currentDisplayFilters = this.savedfilters;
        }

        public updateDisplay(newFilters: IFilterItem[], oldFilters: IFilterItem[]) {
            const addedFilters = newFilters.filter(filter => !oldFilters.find(f => f.key === filter.key && f.type === filter.type));
            const removedFilters = oldFilters.filter(filter => !newFilters.find(f => f.key === filter.key && f.type === filter.type));
            this.setFilterDisplay(addedFilters, true);
            this.setFilterDisplay(removedFilters, false);
        }

        private setFilterDisplay(filters: Array<IFilterItem> = [], isAdded: boolean): void {
            const classScope = this;

            //Assumption:  All filters of a certain type are located in the same container.
            Array.from(new Set(filters.map(filter => filter.type))).forEach(type => {
                const typeContainer = classScope.$refs[type.toString()] as HTMLElement;

                if (typeContainer) {
                    filters.filter(i => i.type === type).forEach(filter => {
                        !classScope.updateCheckboxDisplay(typeContainer, filter.key, isAdded)
                            && classScope.updateSelectDisplay(typeContainer, filter.key, isAdded);
                    });
                }
            });
        }

        private updateCheckboxDisplay(typeContainer: HTMLElement, key: string, isAdded: boolean): boolean {
            const input = typeContainer.querySelector(`input[value='${key}']`) as HTMLInputElement;

            if (input && input.type.toLowerCase() === "checkbox") {
                input.checked = isAdded;
                return true;
            }
            return false;
        }

        private updateSelectDisplay(typeContainer: HTMLElement, key: string, isAdded: boolean): boolean {
            const select = typeContainer.querySelector("select") as HTMLSelectElement;

            if (select) {
                isAdded ? select.value = key : select.selectedIndex = 1;
                return true;
            }
            return false;
        }

        private getFilterHistory(): void {
            const storageItem = this.storageService.retrieve(constants.storageKey);

            if (this.filterHistoryService.hasMismatchedFilters()) {
                this.setImmediateRefreshRequired(true);
                this.updateDisplay(storageItem.filters, this.filterHistoryService.serverFilters);
            }

            const distinctTypes = [...new Set(storageItem.filters.map(sf => sf.type))];
            for (let type of distinctTypes) {
                this.updateAccordionByFilter(type);
            }

            this.updateStore(storageItem.filters);
        }

        private updateAccordionByFilter(filterType: FilterType): void {
            this.updateAccordion(FilterType[filterType] + '-Button');
        }
    }
</script>