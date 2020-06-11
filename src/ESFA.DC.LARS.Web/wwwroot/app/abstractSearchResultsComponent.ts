import Vue from "vue";
import { SearchType } from './Enums/SearchType';
import { ResultsHelper } from './Helpers/resultsHelper';
import LinkService from './Services/LinkService';
import { IFilterItem } from "./Interfaces/IFilterItem";
import { ISearchResults } from "./Interfaces/ISearchResults";
import { FilterStoreService } from "./Services/filterStoreService";
import { constants } from './constants';
import { enumHelper } from './Helpers/enumHelper';
import { debounce } from 'vue-debounce';
import StorageService from './Services/storageService';
import { IStorageItem } from "./Interfaces/IStorageItem";

export default abstract class AbstractSearchResultsComponent extends Vue {

    abstract async getDataAsync(filters: Array<IFilterItem>): Promise<ISearchResults>;
    abstract getSearchType(): SearchType;

    private immediateRefresh: boolean = false;
    private linkService: LinkService;
    private storageService: StorageService;
    protected filterStoreService: FilterStoreService;
    private resultsHelper!: ResultsHelper;
    private latestRequestId: number = 0;

    constructor() {
        super();
        this.linkService = new LinkService();
        this.storageService = new StorageService(sessionStorage);
        this.filterStoreService = new FilterStoreService(this.getSearchType());
    }

    public intialise(): void {
        this.resultsHelper = new ResultsHelper(this.$refs["Results"] as HTMLElement, this.$refs["ResultsCount"] as HTMLElement, this.$refs["ValidationErrors"] as HTMLElement);
        this.setFilterWatch();
    }

    public get searchTerm(): string {
        return (<HTMLInputElement>document.getElementById("autocomplete-overlay"))?.value;
    };

    public setImmediateRefreshRequired(refreshRequired: boolean): void {
        this.immediateRefresh = refreshRequired;
    }

    public learningTypeChanged(value: string): void {
        const clientSearchType = enumHelper.ConvertServerSearchTypeEnumToClientType(value);
        const storageItem = this.storageService.retrieve(constants.storageKey) as IStorageItem;

        storageItem.filters = this.filterStoreService.getFiltersForNewSearch(clientSearchType, storageItem);
        window.location.href = this.linkService.getResultsLinkForSearchType(clientSearchType, storageItem);

        this.storageService.updateFilters(constants.storageKey, storageItem.filters);
    }

    public search(event: Event): void {
        event.preventDefault();
        this.resultsHelper.setIsLoading();
        this.getResultsAsync(this.getDataAsync);
    }

    private setFilterWatch(): void {
        const debouncedCallback = debounce(async () => { await this.getResultsAsync(this.getDataAsync) }, constants.debounceTime);
        const classScope = this;

        const wrappedCall = function () {
            classScope.resultsHelper.setIsLoading();
            debouncedCallback();
        }

        const needsClientSideRefresh = this.immediateRefresh || this.linkService.hasFilterQueryStringParam(window.location.search);
        this.filterStoreService.watchFilters(wrappedCall, needsClientSideRefresh, true);
    }

    private async getResultsAsync(getDataAsync: Function) {
        this.latestRequestId++;
        const classScope = this;
        const getResults = async function (requestId: number) {
            const response = await getDataAsync();
            // Only update results if no subsequent requests have been made.
            if (requestId === classScope.latestRequestId) {
                classScope.resultsHelper.updateForResponse(response);
            }
        };
        await getResults(this.latestRequestId);
    }
}
