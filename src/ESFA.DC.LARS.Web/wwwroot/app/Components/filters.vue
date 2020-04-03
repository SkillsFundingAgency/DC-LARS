<script lang="ts">
    import { Component, Vue, Prop } from 'vue-property-decorator';
    import { IFilterItem, FilterType } from '../../app/Interfaces/IFilterItem';
    import { filterService } from '../Services/filterService';
    import { filterStoreService } from '../Services/filterStoreService';
    import { accordionService } from '../Services/accordionService';
    import { SearchType } from '../SearchType';
    import StorageService from '../Services/storageService';
    import { IStorageItem } from '../Interfaces/IStorageItem';

    @Component({
        template: "#filtersTemplate"
    })
    export default class Filters extends Vue {
        @Prop() public searchType!: SearchType;
        private currentDisplayFilters: Array<IFilterItem> = [];
        private storageService : StorageService;
        private storageKey : string = 'sessionData';

        constructor() {
            super();

            this.storageService = new StorageService(sessionStorage);
        }

        mounted() {
            this.getFilterHistory();

            this.currentDisplayFilters = this.savedfilters;
            filterStoreService.watchFilters(this.searchType, this.updateDisplay, false, true);
            accordionService.initialiseAccordion();
        }

        get savedfilters(): Array<IFilterItem> {
            return [...filterStoreService.getSavedFilters(this.searchType)];
        };

        public clearFilters(): void {
            filterStoreService.updateStore(this.searchType, []);
            this.storageService.clearFilters(this.storageKey);
            this.updateDisplay();
        }

        public updateCheckboxFilter(key: string, value: string, isChecked: boolean, type: FilterType): void {
            const filters = this.savedfilters; 
            const actionedFilter: IFilterItem = { key, value, type };

            isChecked ? filters.push(actionedFilter) : filterService.removeFilterFromArray(filters, actionedFilter);

            this.storageService.updateFilters(this.storageKey, filters);
            this.updateStore(filters);
        }

         public updateSelectFilter(key: string, value: string, type: FilterType): void {
            const filters = this.savedfilters; 
            let filter = filterService.findFilterItemByType(type, filters);

            filter ? filter = Object.assign(filter, {key: key, value: value}) : filters.push({ key, value, type });
            
            this.storageService.updateFilters(this.storageKey, filters);
            this.updateStore(filters);
        }

        public updateAccordion(id: string) : void {
            accordionService.toggleSection(id, false);
        }

        public updateAccordionAll(): void {
            accordionService.updateAccordionAll();
        }

        private updateStore(filters: Array<IFilterItem>) {
            filterStoreService.updateStore(this.searchType, filters);
            this.currentDisplayFilters = this.savedfilters;
        }

        public updateDisplay() {
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

                if (typeContainer) {
                    filters.filter(i => i.type === type).forEach(filter => {
                        !classScope.updateCheckboxDisplay(typeContainer, filter.key, isAdded)
                            && classScope.updateSelectDisplay(typeContainer, filter.key, isAdded);
                    });
                }
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
                isAdded ? select.value = key : select.selectedIndex = 1;
                return true;
            }
            return false;
        }

        private getFilterHistory() : void {
            const sessionData = this.storageService.retrieve(this.storageKey) as IStorageItem;

            if (sessionData) {
                const filters = sessionData.filters;
                const storeFilters = new Array<IFilterItem>();

                if (filters) {
                    for (let filter of filters) {
                        
                        //empty homepage filter
                        if (filter.key === '' && filter.value === '') {
                            continue;
                        }

                        if (filter.type !== FilterType.TeachingYears) {
                            let checkbox = null;

                            //try to handle free text input from home page :(
                            if (filter.key === '') { 
                                const checkboxes = document.querySelectorAll("input[type='checkbox']");
                                checkboxes.forEach(cbox => {
                                    const cboxElement = cbox as HTMLInputElement;
                                    if (cboxElement.value.includes(filter.value)) {
                                        checkbox = cboxElement;
                                    }
                                });
                            }
                            else {
                                checkbox = document.querySelector("input[type='checkbox'][value='" + filter.key + "']") as HTMLInputElement;
                            }

                            if (checkbox) {
                                if (!this.storeContainsFilter(filter)) {
                                    storeFilters.push(filter);
                                }
                                this.updateAccordionByFilter(filter.type);
                            }
                        }
                        else { //handle teaching year select
                            const teachingYearElement = document.getElementById(filter.key) as HTMLOptionElement;

                            if (teachingYearElement) {
                                if (!this.storeContainsFilter(filter)) {
                                    storeFilters.push(filter);
                                }
                                this.updateAccordionByFilter(filter.type);
                            }
                        }
                    }

                    this.currentDisplayFilters = storeFilters;
                    this.updateDisplay();
                    this.updateStore(storeFilters);
                }
            }
        }

        private storeContainsFilter(filter : IFilterItem): boolean {
            return this.savedfilters.includes(filter);
        }

        private updateAccordionByFilter(filterType : FilterType) : void {
            this.updateAccordion(FilterType[filterType] + 'Button');
        }
    }
</script>