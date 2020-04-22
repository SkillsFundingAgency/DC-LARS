﻿import { IStorageItem } from '../Interfaces/IStorageItem';
import StorageService from './storageService';
import { IFilterItem, FilterType } from '../Interfaces/IFilterItem';
import { formHelper } from '../Helpers/formHelper';
import { INameValue } from '../Interfaces/INameValue';
import { constants } from '../constants';

export default class LinkService {

    setLinks() {
        const storageService = new StorageService(sessionStorage);
        const storageItem = storageService.retrieve(constants.storageKey) as IStorageItem;

        this.renderBreadcrumbs(storageItem.frameworkSearch);

        this.setAnchorLinkById("homeLink", "/");
        this.setLearningAimSearchResultsLink(storageItem.searchTerm, storageItem.teachingYear, storageItem.filters);
        this.setFrameworksSearchResultsLink(storageItem.searchTerm, storageItem.filters);
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

    private setLearningAimSearchResultsLink(searchTerm: string, teachingYear: string, filters : IFilterItem[]) {
        const anchor = document.getElementById("searchResultsLink") as HTMLAnchorElement;

        if (anchor) {
            anchor.href = "#";
            const formElements = this.getSearchAndFilterElements(searchTerm, filters);
            // If no teaching year filter applied then use default.
            if (!formElements.find(f => f.name === FilterType.TeachingYears)) {
                formElements.push({ name: FilterType.TeachingYears, value: teachingYear });
            }
            this.addAnchorSubmitEvent(anchor, '/LearningAimSearchResult/Search', formElements);
        }
    }

    private setFrameworksSearchResultsLink(searchTerm: string, filters: IFilterItem[]) {
        const anchor = document.getElementById("frameworksSearchResultsLink") as HTMLAnchorElement;
        if (anchor) {
            anchor.href = "#";
            this.addAnchorSubmitEvent(anchor, '/FrameworkSearchResult/Search', this.getSearchAndFilterElements(searchTerm, filters));
        }
    }

    private getSearchAndFilterElements(searchTerm: string, filters: IFilterItem[]): Array<INameValue<string>> {
        const formElements: Array<INameValue<string>> = filters.map(f => ({ name:f.type, value: f.key } as INameValue<string>));
        formElements.push({ name: "SearchTerm", value: searchTerm });
        return formElements;
    }

    private addAnchorSubmitEvent(anchor: HTMLAnchorElement, formAction: string, formElements: Array<INameValue<string>>) : void {
        anchor.addEventListener("click", function () {
            const form = document.getElementById("breadcrumbSubmit") as HTMLFormElement;
            form.action = formAction;
            formHelper.addInputElements(form, formElements);
            form.submit();
        });
    }
}