﻿import { IStorageItem } from '../Interfaces/IStorageItem';
import StorageService from './storageService.js';
import { IFilterItem } from '../Interfaces/IFilterItem';

export default class LinkService {

    setLinks() {
        const storageService = new StorageService(sessionStorage);
        const storageItem = storageService.retrieve('sessionData') as IStorageItem;

        this.renderBreadcrumbs(storageItem.frameworkSearch);

        this.setAnchorLinkById("homeLink", "/");
        this.setAnchorLinkById("searchResultsLink", `/LearningAimSearchResult?SearchTerm=${storageItem.searchTerm}&TeachingYear=${storageItem.teachingYear}`);
        this.setAnchorLinkById("frameworksSearchResultsLink", `/FrameworkSearchResult?SearchTerm=${storageItem.searchTerm}`);
        this.setAnchorLinkById("learningAimDetailLink", `/LearningAimDetails/${storageItem.learnAimRef}?academicYear=${storageItem.teachingYear}`);
        this.setAnchorLinkById("frameworksLink", `/Frameworks/${storageItem.learnAimRef}`);
        this.setAnchorLinkById("pathwaysLink", `/FrameworkDetails/${storageItem.frameworkCode}/${storageItem.programType}/${storageItem.pathwayCode}`);

        this.setLearningAimDetailText(storageItem.learningAimTitle);
    }

    private renderBreadcrumbs(frameworkSearch: boolean) {
        const frameworkAnchor = document.getElementById("frameworksBreadcrumbs") as HTMLAnchorElement;
        const learningAimAnchor = document.getElementById("learningAimBreadcrumbs") as HTMLAnchorElement;

        if (frameworkSearch && frameworkAnchor) {
            frameworkAnchor.removeAttribute("style");
            if (learningAimAnchor) {
                const parent = learningAimAnchor.parentNode as Node;
                parent.removeChild(learningAimAnchor);
            }

            return;
        }

        if (learningAimAnchor) {
            learningAimAnchor.removeAttribute("style");
            if (frameworkAnchor) {
                const parent = frameworkAnchor.parentNode as Node;
                parent.removeChild(frameworkAnchor);
            }
        }
    }

    private setAnchorLinkById(linkId: string, href: string) {
        const anchor = document.getElementById(linkId) as HTMLAnchorElement;

        if (anchor) {
            anchor.href = href;
        }
    }

    private setLearningAimDetailText(learningAimTitle: string) {
        const frameworksAnchor = document.getElementById("frameworksLink") as HTMLAnchorElement;
        const learningAimDetailAnchor = document.getElementById("learningAimDetailLink") as HTMLAnchorElement;

        if (frameworksAnchor && learningAimDetailAnchor) {
            learningAimDetailAnchor.innerHTML = learningAimTitle;
        }
    }
}