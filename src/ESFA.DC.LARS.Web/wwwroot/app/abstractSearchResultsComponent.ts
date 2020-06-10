import Vue from "vue";
import { SearchType } from './Enums/SearchType';
import { ResultsHelper } from './Helpers/resultsHelper';
import LinkService from './Services/LinkService';
import { IFilterItem } from "./Interfaces/IFilterItem";
import { ISearchResults } from "./Interfaces/ISearchResults";
import { FilterStoreService } from "./Services/filterStoreService";

export default abstract class AbstractSearchResultsComponent extends Vue {

    abstract async getDataAsync(filters: Array<IFilterItem>): Promise<ISearchResults>;
    abstract searchType: SearchType;

    private immediateRefresh: boolean = false;
    private linkService: LinkService;
    private resultsHelper!: ResultsHelper;
    private filterStoreService!: FilterStoreService;

    constructor() {
        super();
        this.linkService = new LinkService();
    }

    public intialise() {
        this.filterStoreService = new FilterStoreService(this.searchType);
        this.resultsHelper = new ResultsHelper(this.$refs["Results"] as HTMLElement, this.$refs["ResultsCount"] as HTMLElement, this.$refs["ValidationErrors"] as HTMLElement);
        const needsClientRefresh = this.immediateRefresh || this.linkService.hasFilterQueryStringParam(window.location.search);
        this.resultsHelper.manageResults(this.getDataAsync, this.searchType, needsClientRefresh);
    }

    public get searchTerm(): string {
        return (<HTMLInputElement>document.getElementById("autocomplete-overlay"))?.value;
    };

    public get savedFilters(): Array<IFilterItem> {
        return this.filterStoreService.getSavedFilters();
    }

    public setImmediateRefreshRequired(refreshRequired: boolean) {
        this.immediateRefresh = refreshRequired;
    }

    public learningTypeChanged(value: string) {
        this.linkService.redirectToResults(value, this.searchType);
    }

    public search(event: Event) {
        event.preventDefault();
        this.resultsHelper.setIsLoading();
        this.resultsHelper.getResultsAsync(this.getDataAsync);
    }
}
