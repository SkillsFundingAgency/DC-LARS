import { IStorageItem } from '../Interfaces/IStorageItem';
import { IFilterItem } from '../Interfaces/IFilterItem';
import { FilterType } from '../Enums/FilterType';
import { SearchType } from '../Enums/SearchType';

export default class LinkService {
  
    public getQualificationsSearchResultsLink(storageItem: IStorageItem): string {
        return `/LearningAimSearchResult?SearchTerm=${storageItem.searchTerm}&TeachingYear=${this.getTeachingYear(storageItem)}${this.hasFiltersParam(storageItem.filters)}`;
    }

    public getResultsLinkForSearchType(searchType: SearchType, storageItem: IStorageItem ): string {

        switch (searchType) {
            case SearchType.Frameworks:
                return this.getFrameworksSearchResultsLink(storageItem);
            case SearchType.Units:
                return this.getUnitsSearchResultsLink(storageItem);
            case SearchType.Qualifications:
               return this.getQualificationsSearchResultsLink(storageItem);
            case SearchType.Standards:
                return this.getStandardsSearchResultsLink(storageItem);
            default:
                return "/";
        }
    }

    public getQualificationsDetailsLink(storageItem: IStorageItem): string {
        return `/LearningAimDetails/${storageItem.learnAimRef}?academicYear=${this.getTeachingYear(storageItem)}`;
    }

    public getQualificationsDetailsLinkWithoutYear(storageItem: IStorageItem): string {
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

    public getStandardsRelatedAimsLink(storageItem: IStorageItem): string {
        return `/StandardLearningAims/${storageItem.standardCode}`;
    }

    public hasFilterQueryStringParam(url: string): boolean {
        const urlParams = new URLSearchParams(url);
        return Boolean(urlParams.get('hasFilters'));
    }

    private getTeachingYear(storageItem: IStorageItem): string {
        const teachingFilter = storageItem.filters.find(f => f.type === FilterType.TeachingYears);
        return teachingFilter ? teachingFilter.key : storageItem.currentAcademicYear;
    }

    private hasFiltersParam(filters: IFilterItem[]): string {
        if (filters.some(f => f.type !== FilterType.TeachingYears)) {
            return "&hasFilters=true";
        }
        return '';
    }
}