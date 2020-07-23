<script lang="ts">
    import { Component, Vue, Prop } from 'vue-property-decorator';
    import { IFilterItem } from '../../app/Interfaces/IFilterItem';
    import { filterService } from '../Services/filterService';
    import { FilterStoreService } from '../Services/filterStoreService';
    import { accordionService } from '../Services/accordionService';
    import { SearchType } from '../enums/SearchType';
    import { FilterType } from '../enums/FilterType';
    import StorageService from '../Services/storageService';
    import FilterHistoryService from '../Services/filterHistoryService';
    import { constants } from '../constants';
    import { debounce } from 'vue-debounce';

    @Component({
        template: "#filtersTemplate"
    })
    export default class Filters extends Vue {
        @Prop() public searchType!: SearchType;

        private currentDisplayFilters: Array<IFilterItem> = [];
        private storageService: StorageService;
        private filterHistoryService: FilterHistoryService;
        private filterStoreService: FilterStoreService;

        constructor() {
            super();
            this.filterHistoryService = new FilterHistoryService();
            this.storageService = new StorageService(sessionStorage);
            this.filterStoreService = new FilterStoreService(this.searchType);
        }

        mounted() {
            accordionService.initialiseAccordion();
            this.syncFiltersAndUpdateDisplay();
            this.currentDisplayFilters = this.savedfilters;
            this.setFilterTextVisible();
            this.filterStoreService.watchFilters(() => this.updateDisplay(this.savedfilters, this.currentDisplayFilters), false, true);
        }

        get savedfilters(): Array<IFilterItem> {
            return [...this.filterStoreService.getSavedFilters()];
        };

        public clearFilters(): void {
            this.filterStoreService.updateStore([]);
            this.storageService.clearFilters(constants.storageKey);
            this.updateDisplay(this.savedfilters, this.currentDisplayFilters);
            this.currentDisplayFilters = [];
        }

        public filterItems(event: Event): void {
            debounce(() => { 
                const target = event.target as HTMLInputElement;
                const container = target.closest(".filter-section");
                const filterItems = container?.getElementsByClassName("filter-item");

                if (filterItems) {
                    const textToFilterBy = target?.value.toUpperCase();

                    for (let i = 0; i < filterItems.length; i++) {
                        const label = filterItems[i].getElementsByTagName("label")[0];

                        if (label.innerText.toUpperCase().indexOf(textToFilterBy) > -1) {
                            (filterItems[i] as HTMLElement).style.display = '';
                        } else {
                            (filterItems[i] as HTMLElement).style.display = "none";
                        }
                    }
                }
            }, "100ms")();

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

        public updateAccordion(event: Event): void {
            const target = event.target as HTMLElement;
            if (target) {
                accordionService.toggleSection(target.id, false);
            }
            event.preventDefault();
        }

        public updateAccordionAll(): void {
            accordionService.updateAccordionAll();
        }

        private updateStore(filters: Array<IFilterItem>) {
            this.filterStoreService.updateStore(filters);
            this.currentDisplayFilters = this.savedfilters;
        }

        public updateDisplay(newFilters: IFilterItem[], oldFilters: IFilterItem[]) {
            const addedFilters = newFilters.filter(filter => !oldFilters.find(f => f.key === filter.key && f.type === filter.type));
            const removedFilters = oldFilters.filter(filter => !newFilters.find(f => f.key === filter.key && f.type === filter.type));
            this.setFilterDisplay(addedFilters, true);
            this.setFilterDisplay(removedFilters, false);
        }

        private setFilterTextVisible(): void {
            var filterTextContainers = document.getElementsByClassName("filter-text-container");
            for (let i = 0; i < filterTextContainers.length; i++) {
                (filterTextContainers[i] as HTMLElement).style.display = '';
            }
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

        private syncFiltersAndUpdateDisplay(): void {
            const storageItem = this.storageService.retrieve(constants.storageKey);

            // Check if current clientside filters and server filters used to render page are the same. 
            // If not (can happen on f5 refresh) then refresh results with client filters.
            storageItem.hasFilterMismatch = this.filterHistoryService.hasMismatchedFilters();

            if (storageItem.hasFilterMismatch) {
                this.updateDisplay(storageItem.filters, storageItem.serverFilters);
            }

            const distinctTypes = [...new Set(storageItem.filters.map(sf => sf.type))];
            for (let type of distinctTypes) {
                this.updateAccordionByFilter(type);
            }

            this.storageService.store(constants.storageKey, storageItem);
            this.updateStore(storageItem.filters);
        }

        private updateAccordionByFilter(filterType: FilterType): void {
             accordionService.toggleSection(FilterType[filterType] + '-Button', false);
        }
    }
</script>