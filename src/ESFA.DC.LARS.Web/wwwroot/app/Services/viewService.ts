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
        this.setupLearningAimsResultView(searchTerm, currentAcademicYear, SearchType.Qualifications);
    }

    public setupUnitsResultView(searchTerm: string, currentAcademicYear: string): void {
        this.setupLearningAimsResultView(searchTerm, currentAcademicYear, SearchType.Units);
    }

    private setupLearningAimsResultView(searchTerm: string, currentAcademicYear: string, searchType: SearchType) : void {
        this.removeFilterNonScriptTag();
        const storageItem = Object.assign(this.currentStorageItem, { searchTerm, currentAcademicYear, searchType }); 
        this.storageService.store(constants.storageKey, storageItem);
        this.breadcrumbService.buildBreadcrumb(storageItem, Page.Results);
    }

    public setupLearningAimDetailView(learnAimRef: string, learningAimTitle: string): void {
        const storageItem = Object.assign(this.currentStorageItem, { learnAimRef, learningAimTitle}); 
        this.storageService.store(constants.storageKey, storageItem);
        this.breadcrumbService.buildBreadcrumb(storageItem, Page.LearningAimDetails);
    }

    public setupFrameworkView(): void {
        this.breadcrumbService.buildBreadcrumb(this.currentStorageItem, Page.Pathway);
    }

    public setupLearningAimCategoryView(): void {
        this.breadcrumbService.buildBreadcrumb(this.currentStorageItem, Page.Category);
    }

    public setupCommonComponentView(frameworkCode: string, programType: string, pathwayCode: string): void {
        const storageItem = Object.assign(this.currentStorageItem, { frameworkCode, programType, pathwayCode }); 
        this.storageService.store(constants.storageKey, storageItem);
        this.breadcrumbService.buildBreadcrumb(storageItem, Page.CommonComponent);
    }

    public setupFrameworkSearchResultView(searchTerm: string): void {
        this.removeFilterNonScriptTag();
        const storageItem = Object.assign(this.currentStorageItem, { searchTerm, searchType: SearchType.Frameworks });
        this.storageService.store(constants.storageKey, storageItem);
        this.breadcrumbService.buildBreadcrumb(storageItem, Page.Results);
    }

    public setupFrameworkDetailView(frameworkCode: string, programType: string, pathwayCode: string): void {
        const storageItem = Object.assign(this.currentStorageItem, { frameworkCode, programType, pathwayCode });
        this.storageService.store(constants.storageKey, storageItem);
        this.breadcrumbService.buildBreadcrumb(storageItem, Page.Pathway);
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

    private removeFilterNonScriptTag() {
        const element = document.getElementById("filtersNoScript") as HTMLElement;
        if (element && element.parentNode) {
            element.parentNode.removeChild(element);
        }
    }

    private get currentStorageItem(): IStorageItem {
        return this.storageService.retrieve(constants.storageKey) as IStorageItem;
    };

}