﻿import { IStorageItem } from '../Interfaces/IStorageItem';
import StorageService from './storageService.js';
import { IFilterItem } from '../Interfaces/IFilterItem';

export default class LinkService {

    setLinks() {
        const storageService = new StorageService(sessionStorage);
        const storageItem = storageService.retrieve('sessionData') as IStorageItem;

        this.renderBreadcrumbs(storageItem.frameworkSearch);
        this.setHomeLink();
        this.setLearningAimSearchResultsLink(storageItem.searchTerm, storageItem.teachingYear, storageItem.filters);
        this.setFrameworksSearchResultsLink(storageItem.searchTerm, storageItem.filters);
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

    private setLearningAimSearchResultsLink(searchTerm: string, teachingYear: string, filters : IFilterItem[]) {
        const anchor = document.getElementById("searchResultsLink") as HTMLAnchorElement;

        if (anchor != null) {
            anchor.href = "#";
            const that = this;
            anchor.addEventListener("click", function () {
                const form = document.getElementById("breadcrumbSubmit") as HTMLFormElement;
                form.action = '/LearningAimSearchResult/Search';

                that.addElement("SearchTerm", searchTerm, form);
                that.addElement("TeachingYears", teachingYear, form);
                filters.forEach(f => that.addElement(f.type.toString(), f.key, form));

                form.submit();
            });
        }
    }

    private setFrameworksSearchResultsLink(searchTerm: string, filters: IFilterItem[]) {
        const anchor = document.getElementById("frameworksSearchResultsLink") as HTMLAnchorElement;

        if (anchor != null) {
            anchor.href = "#";
            const that = this;

            anchor.addEventListener("click", function () {
                const form = document.getElementById("breadcrumbSubmit") as HTMLFormElement;
                form.action = '/FrameworkSearchResult/Search';
                that.addElement("SearchTerm", searchTerm, form);

                filters.forEach(f => that.addElement(f.type.toString(), f.key, form));
                form.submit();
            });
        }
    }

    private addElement(name: string, value: string, form: HTMLFormElement) {
        const element = <HTMLInputElement>(document.createElement('input'));
        Object.assign(element, { name, value });
        form.appendChild(element);
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