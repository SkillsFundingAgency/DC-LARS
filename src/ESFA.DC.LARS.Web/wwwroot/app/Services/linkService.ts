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

    public redirectToResults(serverSearchType: string, oldSearchResults: SearchType): void {
        const linkService = new LinkService();
        const clientSearchType = enumHelper.ConvertServerEnumValueToClientEnum(serverSearchType);
        let updatedFilters: Array<IFilterItem> = [];
        const storageItem = this.storageService.retrieve(constants.storageKey) as IStorageItem;

        if (clientSearchType === SearchType.Frameworks) {
            window.location.href = linkService.getFrameworksSearchResultsLink(storageItem);
        }
        else {
            if (clientSearchType === SearchType.Units) {
                window.location.href = linkService.getUnitsSearchResultsLink(storageItem);
            }

            if (clientSearchType === SearchType.Qualifications) {
                window.location.href = linkService.getQualificationsSearchResultsLink(storageItem);
            }

            // If moving to a search that has teaching years then keep exisiting teaching year
            // filter or use current academic year if not present.
            const filters = filterStoreService.getSavedFilters(oldSearchResults);
            if (filters.some(f => f.type === FilterType.TeachingYears)) {
                updatedFilters = filters.filter(f => f.type === FilterType.TeachingYears);
            } else {
                const storageItem = this.storageService.retrieve(constants.storageKey) as IStorageItem;
                updatedFilters.push({ type: FilterType.TeachingYears, key: storageItem.currentAcademicYear, value: '' });
            }
        }

        this.storageService.updateFilters(constants.storageKey, updatedFilters);
    }

    public getQualificationsSearchResultsLink(storageItem: IStorageItem): string {
        return `/LearningAimSearchResult?SearchTerm=${storageItem.searchTerm}&TeachingYear=${this.getTeachingYear(storageItem)}${this.hasFiltersParam(storageItem.filters)}`;
    }

    public getQualificationsDetailsLink(storageItem: IStorageItem): string {
        return `/LearningAimDetails/${storageItem.learnAimRef}?academicYear=${this.getTeachingYear(storageItem)}`;
    }

    public getQualificationsDetailsLinkForFrameworks(storageItem: IStorageItem): string {
        //TODO:  This can be removed once frameworks accept teaching year.
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

    private getTeachingYear(storageItem: IStorageItem): string {
        const teachingFilter = storageItem.filters.find(f => f.type === FilterType.TeachingYears);
        if (teachingFilter) {
            return teachingFilter.key;
        }
        return storageItem.currentAcademicYear;
    }

    private hasFiltersParam(filters: IFilterItem[]): string {
        if (filters.some(f => f.type !== FilterType.TeachingYears)) {
            return "&hasFilters=true";
        }
        return '';
    }
}