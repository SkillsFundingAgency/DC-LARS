import { IStorageItem } from '../Interfaces/IStorageItem';
import StorageService from './storageService';
import LinkService from './linkService';
import { IFilterItem, FilterType } from '../Interfaces/IFilterItem';

export class ViewService {

    private readonly storageService: StorageService;
    private readonly linkService: LinkService;
    private readonly sessionData = "sessionData";

    constructor() {
        this.storageService = new StorageService(sessionStorage);
        this.linkService = new LinkService();
    }

    setupLearningAimSearchResultView(searchTerm: string, teachingYear: string) {
        this.removeFilterNonScriptTag();
        const storageItem = this.storageService.retrieve(this.sessionData) as IStorageItem;
        storageItem.searchTerm = searchTerm;
        storageItem.teachingYear = teachingYear;
        storageItem.frameworkSearch = false;
        this.storageService.store(this.sessionData, storageItem);
        this.linkService.setLinks();
    }

    setupLearningAimDetailView(learnAimRef: string, learningAimTitle: string, teachingYear: string) {
        const storageItem = this.storageService.retrieve(this.sessionData) as IStorageItem;

        storageItem.learnAimRef = learnAimRef;
        storageItem.learningAimTitle = learningAimTitle;
        storageItem.learningAimDetailsYear = teachingYear;

        this.storageService.store(this.sessionData, storageItem);
        this.linkService.setLinks();
    }

    setupFrameworkView() {
        this.linkService.setLinks();
    }

    setupLearningAimCategoryView() {
        this.linkService.setLinks();
    }

    setupFrameworkSearchResultView(searchTerm: string) {
        this.removeFilterNonScriptTag();
        const storageItem = this.storageService.retrieve(this.sessionData) as IStorageItem;
        storageItem.searchTerm = searchTerm;
        storageItem.frameworkSearch = true;

        this.storageService.store(this.sessionData, storageItem);
        this.linkService.setLinks();
    }

    setupFrameworkDetailView(frameworkCode: string, programType: string, pathwayCode: string) {
        const storageItem = this.storageService.retrieve(this.sessionData) as IStorageItem;

        storageItem.frameworkCode = frameworkCode;
        storageItem.programType = programType;
        storageItem.pathwayCode = pathwayCode;

        this.storageService.store(this.sessionData, storageItem);
        this.linkService.setLinks();
    }

    getLearningAimFilters(awardingBodies: string[], levels: string[], teachingYears: string[], fundingStreams: string[]): IFilterItem[] {
        let filters: IFilterItem[] = [];

        filters = filters.concat(awardingBodies.map(f => ({ type: FilterType.AwardingBodies, key: f, value: '' } as IFilterItem)));
        filters = filters.concat(levels.map(f => ({ type: FilterType.Levels, key: f, value: '' } as IFilterItem)));
        filters = filters.concat(teachingYears.map(f => ({ type: FilterType.TeachingYears, key: f, value: '' } as IFilterItem)));
        filters = filters.concat(fundingStreams.map(f => ({ type: FilterType.FundingStreams, key: f, value: '' } as IFilterItem)));

        return filters;
    }

    getFrameworkFilters(frameworkTypes: string[], issuingAuthorities: string[]): IFilterItem[] {
        let filters: IFilterItem[] = [];

        filters = filters.concat(frameworkTypes.map(f => ({ type: FilterType.FrameworkTypes, key: f, value: '' } as IFilterItem)));
        filters = filters.concat(issuingAuthorities.map(f => ({ type: FilterType.IssuingAuthorities, key: f, value: '' } as IFilterItem)));

        return filters;
    }

    removeFilterNonScriptTag() {
        const element = document.getElementById("filtersNoScript") as HTMLElement;
        if (element && element.parentNode) {
            element.parentNode.removeChild(element);
        }
    }
}