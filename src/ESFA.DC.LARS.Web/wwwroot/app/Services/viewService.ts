import { IStorageItem } from '../Interfaces/IStorageItem';
import StorageService from './storageService';
import { IFilterItem } from '../Interfaces/IFilterItem';
import { FilterType } from '../Enums/FilterType';
import { SearchType } from '../Enums/SearchType';
import { BreadcrumbService } from '../Breadcrumbs/breadcrumbService';
import { Page } from '../Enums/Page';
import { constants } from '../constants';

export class ViewService {
    private readonly storageService: StorageService;
    private readonly breadcrumbService: BreadcrumbService;

    constructor() {
        this.storageService = new StorageService(sessionStorage);
        this.breadcrumbService = new BreadcrumbService();
    }

    public setupQualificationsResultView(searchTerm: string, currentAcademicYear: string): void {
        this.setupResultsView(Object.assign(this.currentStorageItem, { searchTerm, currentAcademicYear, searchType: SearchType.Qualifications, page: Page.Results }));
    }

    public setupUnitsResultView(searchTerm: string, currentAcademicYear: string): void {
        this.setupResultsView(Object.assign(this.currentStorageItem, { searchTerm, currentAcademicYear, searchType: SearchType.Units, page: Page.Results }));
    }

    public setupStandardsResultView(searchTerm: string): void {
        this.setupResultsView(Object.assign(this.currentStorageItem, { searchTerm, searchType: SearchType.Standards, page: Page.Results }));
    }

    public setupTLevelsResultView(searchTerm: string): void {
        this.setupResultsView(Object.assign(this.currentStorageItem, { searchTerm, searchType: SearchType.TLevels, page: Page.Results }));
    }

    public setupFrameworkSearchResultView(searchTerm: string): void {
        this.setupResultsView(Object.assign(this.currentStorageItem, { searchTerm, searchType: SearchType.Frameworks, page: Page.Results }));
    }

    private setupResultsView(storageItem: IStorageItem) : void {
        this.removeFilterNonScriptTag();
        this.storageService.store(constants.storageKey, storageItem);
        this.breadcrumbService.buildBreadcrumb(storageItem);
    }

    public setupLearningAimDetailView(learnAimRef: string, learningAimTitle: string): void {
        this.setupLearingAimView(learnAimRef, learningAimTitle, Page.LearningAimDetails);
    }

    public setupLearningAimCategoryView(learnAimRef: string, learningAimTitle: string): void {
        this.setupLearingAimView(learnAimRef, learningAimTitle, Page.Category );
    }

    private setupLearingAimView(learnAimRef: string, learningAimTitle: string, page: Page): void {
        const storageItem = Object.assign(this.currentStorageItem, { learnAimRef, learningAimTitle, page: page });
        this.storageService.store(constants.storageKey, storageItem);
        this.breadcrumbService.buildBreadcrumb(storageItem);
    }
  
    public setupStandardDetailsView(standardCode: string, standardName: string): void {
        this.setupStandardView(standardCode, standardName, Page.Standard);
    }

    public setupStandardCommonComponentView(standardCode: string, standardName: string): void {
        this.setupStandardView(standardCode, standardName, Page.CommonComponent);
    }

    public setupStandardLearningAimView(standardCode: string, standardName: string): void {
        this.setupStandardView(standardCode, standardName, Page.RelatedLearningAims);
    }

    private setupStandardView(standardCode: string, standardName: string, page: Page) : void {
        const storageItem = Object.assign(this.currentStorageItem, { standardCode, standardName, page});
        this.storageService.store(constants.storageKey, storageItem);
        this.breadcrumbService.buildBreadcrumb(storageItem);
    }

    public setupFrameworkDetailView(frameworkCode: string, programType: string, pathwayCode: string): void {
        this.setupFrameworkView(frameworkCode, programType, pathwayCode, Page.Pathway);
    }

    public setupFrameworkCommonComponentView(frameworkCode: string, programType: string, pathwayCode: string): void {
        this.setupFrameworkView(frameworkCode, programType, pathwayCode, Page.CommonComponent);
    }

    public setupFrameworkView(frameworkCode: string, programType: string, pathwayCode: string, page:Page): void {
        const storageItem = Object.assign(this.currentStorageItem, { frameworkCode, programType, pathwayCode, page });
        this.storageService.store(constants.storageKey, storageItem);
        this.breadcrumbService.buildBreadcrumb(storageItem);
    }

    public getLearningAimFilters(awardingBodies: string[], levels: string[], teachingYears: string[], fundingStreams: string[]): IFilterItem[] {
        let filters: IFilterItem[] = [];

        filters = filters.concat(awardingBodies.map(f => ({ type: FilterType.AwardingBodies, key: f, value: '' } as IFilterItem)));
        filters = filters.concat(levels.map(f => ({ type: FilterType.Levels, key: f, value: '' } as IFilterItem)));
        filters = filters.concat(teachingYears.map(f => ({ type: FilterType.TeachingYears, key: f, value: '' } as IFilterItem)));
        filters = filters.concat(fundingStreams.map(f => ({ type: FilterType.FundingStreams, key: f, value: '' } as IFilterItem)));

        return filters;
    }

    public getFrameworkFilters(frameworkTypes: string[], issuingAuthorities: string[]): IFilterItem[] {
        let filters: IFilterItem[] = [];
        filters = filters.concat(frameworkTypes.map(f => ({ type: FilterType.FrameworkTypes, key: f, value: '' } as IFilterItem)));
        filters = filters.concat(issuingAuthorities.map(f => ({ type: FilterType.IssuingAuthorities, key: f, value: '' } as IFilterItem)));
        return filters;
    }

    public getStandardsFilters(levels: string[], sectors: string[]): IFilterItem[] {
        let filters: IFilterItem[] = [];
        filters = filters.concat(levels.map(f => ({ type: FilterType.Levels, key: f, value: '' } as IFilterItem)));
        filters = filters.concat(sectors.map(f => ({ type: FilterType.Sectors, key: f, value: '' } as IFilterItem)));
        return filters;
    }

    private removeFilterNonScriptTag() {
        const element = document.getElementById("filtersNoScript") as HTMLElement;
        if (element && element.parentNode) {
            element.parentNode.removeChild(element);
        }
    }

    private get currentStorageItem(): IStorageItem {
        return this.storageService.retrieve(constants.storageKey) as IStorageItem;
    }
}