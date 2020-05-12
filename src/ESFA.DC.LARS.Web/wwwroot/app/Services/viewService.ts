import { IStorageItem } from '../Interfaces/IStorageItem';
import StorageService from './storageService';
import LinkService from './linkService';
import { IFilterItem, FilterType } from '../Interfaces/IFilterItem';
import { SearchType } from '../SearchType';

export class ViewService {

    private readonly storageService: StorageService;
    private readonly linkService: LinkService;
    private readonly sessionData = "sessionData";

    constructor() {
        this.storageService = new StorageService(sessionStorage);
        this.linkService = new LinkService();
    }

    public setupQualificationsResultView(searchTerm: string, teachingYear: string): void {
        this.setupLearningAimsResultView(searchTerm, teachingYear, SearchType.Qualifications);
    }

    public setupUnitsResultView(searchTerm: string, teachingYear: string): void {
        this.setupLearningAimsResultView(searchTerm, teachingYear, SearchType.Units);
    }

    private setupLearningAimsResultView(searchTerm: string, teachingYear: string, searchType: SearchType) : void {
        this.removeFilterNonScriptTag();
        const storageItem = this.storageService.retrieve(this.sessionData) as IStorageItem;
        storageItem.searchTerm = searchTerm;
        storageItem.teachingYear = teachingYear;
        storageItem.searchType = searchType;
        this.storageService.store(this.sessionData, storageItem);
        this.linkService.setLinks();
    }

    public setupLearningAimDetailView(learnAimRef: string, learningAimTitle: string, teachingYear: string): void {

        const storageItem = this.storageService.retrieve(this.sessionData) as IStorageItem;
        storageItem.learnAimRef = learnAimRef;
        storageItem.learningAimTitle = learningAimTitle;
        storageItem.learningAimDetailsYear = teachingYear;

        this.storageService.store(this.sessionData, storageItem);
        this.linkService.setLinks();
    }

    public setupFrameworkView(): void {
        this.linkService.setLinks();
    }

    public setupLearningAimCategoryView(): void {
        this.linkService.setLinks();
    }

    public setupFrameworkSearchResultView(searchTerm: string): void {
        this.removeFilterNonScriptTag();
        const storageItem = this.storageService.retrieve(this.sessionData) as IStorageItem;
        storageItem.searchTerm = searchTerm;
        storageItem.searchType = SearchType.Frameworks;

        this.storageService.store(this.sessionData, storageItem);
        this.linkService.setLinks();
    }

    public setupFrameworkDetailView(frameworkCode: string, programType: string, pathwayCode: string): void {
        const storageItem = this.storageService.retrieve(this.sessionData) as IStorageItem;

        storageItem.frameworkCode = frameworkCode;
        storageItem.programType = programType;
        storageItem.pathwayCode = pathwayCode;

        this.storageService.store(this.sessionData, storageItem);
        this.linkService.setLinks();
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
}