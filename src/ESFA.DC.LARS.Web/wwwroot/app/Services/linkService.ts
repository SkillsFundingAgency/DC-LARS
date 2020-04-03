﻿import { IStorageItem } from '../Interfaces/IStorageItem';
import StorageService from './storageService.js';

export default class LinkService {

    setLinks() {
        const storageService = new StorageService(sessionStorage);
        const storageItem = storageService.retrieve('sessionData') as IStorageItem;

        this.renderBreadcrumbs(storageItem.frameworkSearch);
        this.setHomeLink();
        this.setLearningAimSearchResultsLink(storageItem.searchTerm, storageItem.teachingYear);
        this.setFrameworksSearchResultsLink(storageItem.searchTerm);
        this.setLearningAimDetailLink(storageItem.learnAimRef, storageItem.teachingYear);
        this.setFrameworksLink(storageItem.learnAimRef, storageItem.learningAimTitle);
        this.setPathwaysLink(storageItem.frameworkCode, storageItem.programType, storageItem.pathwayCode);
    }

    private renderBreadcrumbs(frameworkSearch: boolean) {
        const frameworkAnchor = document.getElementById("frameworksBreadcrumbs") as HTMLAnchorElement;
        const learningAimAnchor = document.getElementById("learningAimBreadcrumbs") as HTMLAnchorElement;

        if (frameworkSearch && frameworkAnchor != null) {
            frameworkAnchor.removeAttribute("style");
            if (learningAimAnchor != null) {
                const parent = learningAimAnchor.parentNode as Node;
                parent.removeChild(learningAimAnchor);
            }

            return;
        }

        if (learningAimAnchor != null) {
            learningAimAnchor.removeAttribute("style");
            if (frameworkAnchor != null) {
                const parent = frameworkAnchor.parentNode as Node;
                parent.removeChild(frameworkAnchor);
            }
        }
    }

    private setHomeLink() {
        const anchor = document.getElementById("homeLink") as HTMLAnchorElement;

        if (anchor != null) {
            anchor.href = `/`;
        }
    }

    private setLearningAimSearchResultsLink(searchTerm: string, teachingYear: string) {
        const anchor = document.getElementById("searchResultsLink") as HTMLAnchorElement;

        if (anchor != null) {
            anchor.href = `/LearningAimSearchResult?SearchTerm=${searchTerm}&TeachingYear=${teachingYear}`;
        }
    }

    private setFrameworksSearchResultsLink(searchTerm: string) {
        const anchor = document.getElementById("frameworksSearchResultsLink") as HTMLAnchorElement;

        if (anchor != null) {
            anchor.href = `/FrameworkSearchResult?SearchTerm=${searchTerm}`;
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

    private setPathwaysLink(frameworkCode: string, programType: string, pathwayCode: string) {
        const anchor = document.getElementById("pathwaysLink") as HTMLAnchorElement;

        if (anchor != null) {
            anchor.href = `/FrameworkDetails/${frameworkCode}/${programType}/${pathwayCode}`;
        }
    }

}