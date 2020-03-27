import { IStorageItem } from '../Interfaces/IStorageItem';
import StorageService from './storageService.js';

export default class LinkService {

    setLinks() {
        const storageService = new StorageService(sessionStorage);
        const storageItem = storageService.retrieve('sessionData') as IStorageItem;

        this.setSearchResultsLink(storageItem.searchTerm, storageItem.teachingYear);
        this.setLearningAimDetailLink(storageItem.learnAimRef, storageItem.learningAimDetailsYear);
        this.setFrameworksLink(storageItem.learnAimRef, storageItem.learningAimTitle);
    }

    private setSearchResultsLink(searchTerm: string, teachingYear: string) {
        const anchor = document.getElementById("searchResultsLink") as HTMLAnchorElement;

        if (anchor != null) {
            anchor.href = `/SearchResult?SearchTerm=${searchTerm}&TeachingYear=${teachingYear}`;
        }
    }

    private setLearningAimDetailLink(learnAimRef: string, academicYear: string) {
        const anchor = document.getElementById("learningAimDetailLink") as HTMLAnchorElement;

        if (anchor != null) {
            anchor.href = `/LearningAimDetails/${learnAimRef}?academicYear=${academicYear}`;
        }
    }

    private setFrameworksLink(learnAimRef: string, learningAimTitle: string) {
        const frameworksAnchor = document.getElementById("frameworksLink") as HTMLAnchorElement;
        const learningAimDetailAnchor = document.getElementById("learningAimDetailLink") as HTMLAnchorElement;

        if (frameworksAnchor != null && learningAimDetailAnchor != null) {
            frameworksAnchor.href = `/Frameworks/${learnAimRef}`;
            learningAimDetailAnchor.innerHTML = learningAimTitle;
        }
        
    }

}