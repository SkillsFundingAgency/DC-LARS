﻿import Vue from "vue";
import { SearchType } from './Enums/SearchType';
import { ResultsHelper } from './Helpers/resultsHelper';
import LinkService from './Services/LinkService';
import { ISearchResults } from "./Interfaces/ISearchResults";
import { FilterStoreService } from "./Services/filterStoreService";
import { constants } from './constants';
import { debounce } from 'vue-debounce';
import StorageService from './Services/storageService';
import { IStorageItem } from "./Interfaces/IStorageItem";

export default abstract class AbstractSearchResultsComponent extends Vue {

    abstract async getDataAsync(): Promise<ISearchResults>;
    abstract getSearchType(): SearchType;

    private linkService: LinkService;
    private storageService: StorageService;
    protected filterStoreService: FilterStoreService;
    private resultsHelper!: ResultsHelper;
    private latestRequestId = 0;

    constructor() {
        super();
        this.linkService = new LinkService();
        this.storageService = new StorageService(sessionStorage);
        this.filterStoreService = new FilterStoreService(this.getSearchType());
    }

    public intialise(): void {
        this.resultsHelper = new ResultsHelper(this.$refs["Results"] as HTMLElement, this.$refs["ResultsCount"] as HTMLElement, this.$refs["ValidationErrors"] as HTMLElement, document.getElementById("loadingImage") as HTMLElement);
        this.addFilterWatch();
    }

    public get searchTerm(): string {
        return (<HTMLInputElement>document.getElementById("autocomplete-overlay"))?.value;
    }

    public learningTypeChanged(searchType: SearchType): void {
        const storageItem = this.getStorageItem();

        storageItem.filters = this.filterStoreService.getFiltersForNewSearch(searchType, storageItem);
        window.location.href = this.linkService.getResultsLinkForSearchType(searchType, storageItem);

        this.storageService.updateFilters(constants.storageKey, storageItem.filters);
    }

    public search(event: Event): void {
        event.preventDefault();
        this.resultsHelper.setIsLoading();
        this.refreshResults();
        this.storageService.store(constants.storageKey, Object.assign(this.getStorageItem(), {searchTerm: this.searchTerm}));
    }

    private addFilterWatch(): void {
        const needsClientSideRefresh = this.getStorageItem().hasFilterMismatch || this.linkService.hasFilterQueryStringParam(window.location.search);
        this.filterStoreService.watchFilters(this.getFilterChangeCallback(), needsClientSideRefresh, true);
    }

    private getFilterChangeCallback(): () => void {
        const debouncedCallback = debounce(() => { this.refreshResults() }, constants.debounceTime);

        return() => {
            this.resultsHelper.setIsLoading();
            debouncedCallback();
        }
    }

    private getStorageItem(): IStorageItem  {
        return this.storageService.retrieve(constants.storageKey) as IStorageItem
    }

    private refreshResults() {
        this.latestRequestId++;

        (async (requestId: number) => {
            const response = await this.getDataAsync();
            // Only update results if no subsequent requests have been made.
            if (requestId === this.latestRequestId) {
                this.resultsHelper.updateForResponse(response);
            }
        })(this.latestRequestId);
    }
}
