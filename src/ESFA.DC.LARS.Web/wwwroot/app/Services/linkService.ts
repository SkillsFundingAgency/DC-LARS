import { IStorageItem } from '../Interfaces/IStorageItem';
import StorageService from './storageService';
import { IFilterItem } from '../Interfaces/IFilterItem';
import { FilterType } from '../Enums/FilterType';
import { constants } from '../constants';
import { SearchType } from '../Enums/SearchType';
import { filterStoreService } from './filterStoreService';
import { enumHelper } from '../Helpers/enumHelper';

export default class LinkService {

    private storageService: StorageService;

    constructor() {
        this.storageService = new StorageService(sessionStorage);
    }

    public redirectToResults(newSearchTypeAsServerEnum: string, exisitingSearchType: SearchType): void {
        const clientSearchType = enumHelper.ConvertServerEnumValueToClientEnum(newSearchTypeAsServerEnum);

        const storageItem = this.storageService.retrieve(constants.storageKey) as IStorageItem;
        storageItem.filters = this.updateFiltersForNewSearch(clientSearchType, exisitingSearchType);

        switch (clientSearchType) {
            case SearchType.Frameworks:
                window.location.href = this.getFrameworksSearchResultsLink(storageItem);
                break;
            case SearchType.Units:
                window.location.href = this.getUnitsSearchResultsLink(storageItem);
                break;
            case SearchType.Qualifications:
                window.location.href = this.getQualificationsSearchResultsLink(storageItem);
                break;
            case SearchType.Standards:
                window.location.href = this.getStandardsSearchResultsLink(storageItem);
                break;
            default:
                window.location.href = "/";
        }

        this.storageService.updateFilters(constants.storageKey, storageItem.filters);
    }

    public getQualificationsSearchResultsLink(storageItem: IStorageItem): string {
        return `/LearningAimSearchResult?SearchTerm=${storageItem.searchTerm}&TeachingYear=${this.getTeachingYear(storageItem)}${this.hasFiltersParam(storageItem.filters)}`;
    }

    public getQualificationsDetailsLink(storageItem: IStorageItem): string {
        return `/LearningAimDetails/${storageItem.learnAimRef}?academicYear=${this.getTeachingYear(storageItem)}`;
    }

    public getQualificationsDetailsLinkForFrameworks(storageItem: IStorageItem): string {
        //TODO:  Please use getQualificationsDetailsLink above once teaching year filter added to Frameworks.
        return `/LearningAimDetails/${storageItem.learnAimRef}`;
    }

    public getUnitsSearchResultsLink(storageItem: IStorageItem): string {
        return `/UnitSearchResult?SearchTerm=${storageItem.searchTerm}&TeachingYear=${this.getTeachingYear(storageItem)}${this.hasFiltersParam(storageItem.filters)}`;
    }

    public getUnitsDetailsLink(storageItem: IStorageItem): string {
        return `/UnitDetails/${storageItem.learnAimRef}?academicYear=${this.getTeachingYear(storageItem)}`;
    }

    public getFrameworksSearchResultsLink(storageItem: IStorageItem): string {
        return `/FrameworkSearchResult?SearchTerm=${storageItem.searchTerm}${this.hasFiltersParam(storageItem.filters)}`;
    }

    public getFrameworksDetailsLink(storageItem: IStorageItem): string {
        return `/FrameworkDetails/${storageItem.frameworkCode}/${storageItem.programType}/${storageItem.pathwayCode}`;
    }

    public getStandardsSearchResultsLink(storageItem: IStorageItem): string {
        return `/StandardsSearchResult?SearchTerm=${storageItem.searchTerm}${this.hasFiltersParam(storageItem.filters)}`;
    }

    public getStandardsDetailsLink(storageItem: IStorageItem): string {
        return `/StandardDetails/${storageItem.standardCode}`;
    }

    public hasFilterQueryStringParam(url: string): boolean {
        const urlParams = new URLSearchParams(window.location.search);
        return Boolean(urlParams.get('hasFilters'));
    }

    private getTeachingYear(storageItem: IStorageItem): string {
        const teachingFilter = storageItem.filters.find(f => f.type === FilterType.TeachingYears);
        return teachingFilter ? teachingFilter.key : storageItem.currentAcademicYear;
    }

    private hasTeachingYearFilter(searchType: SearchType) {
        return (searchType === SearchType.Qualifications || searchType === SearchType.Units);
    } 

    private hasFiltersParam(filters: IFilterItem[]): string {
        if (filters.some(f => f.type !== FilterType.TeachingYears)) {
            return "&hasFilters=true";
        }
        return '';
    }

    private updateFiltersForNewSearch(newSearchType: SearchType, exisitingSearchType: SearchType): Array<IFilterItem> {
        let updatedFilters: Array<IFilterItem> = [];

        // If moving to a search that has teaching years then keep exisiting teaching year
        // filter or use current academic year if that not present.
        if (this.hasTeachingYearFilter(newSearchType)) {
            const currentFilters = filterStoreService.getSavedFilters(exisitingSearchType);
            if (currentFilters.some(f => f.type === FilterType.TeachingYears)) {
                updatedFilters = currentFilters.filter(f => f.type === FilterType.TeachingYears);
            } else {
                const storageItem = this.storageService.retrieve(constants.storageKey) as IStorageItem;
                updatedFilters.push({ type: FilterType.TeachingYears, key: storageItem.currentAcademicYear, value: '' });
            }
        }
        return updatedFilters;
    }

}