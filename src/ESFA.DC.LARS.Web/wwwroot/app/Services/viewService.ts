import { IStorageItem } from '../Interfaces/IStorageItem';
import StorageService from './storageService.js';
import LinkService from './linkService.js';

export default class ViewService {

    private readonly storageService: StorageService;
    private readonly linkService: LinkService;
    private readonly sessionData = "sessionData";

    constructor() {
        this.storageService = new StorageService(sessionStorage);
        this.linkService = new LinkService();
    }

    setupLearningAimSearchResultView(searchTerm: string, teachingYear: string) {
        const storageItem = { searchTerm: searchTerm, teachingYear:teachingYear } as IStorageItem;

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
        const storageItem = { searchTerm: searchTerm, frameworkSearch: true } as IStorageItem;

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

}