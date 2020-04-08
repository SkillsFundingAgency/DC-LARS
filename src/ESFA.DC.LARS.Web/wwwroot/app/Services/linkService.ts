import { IStorageItem } from '../Interfaces/IStorageItem';
import StorageService from './storageService';
import { IFilterItem } from '../Interfaces/IFilterItem';

export default class LinkService {

    setLinks() {
        const storageService = new StorageService(sessionStorage);
        const storageItem = storageService.retrieve('sessionData') as IStorageItem;

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

        if (anchor != null) {
            anchor.href = "#";
            const classScope = this;
            anchor.addEventListener("click", function () {
                const form = document.getElementById("breadcrumbSubmit") as HTMLFormElement;
                form.action = '/LearningAimSearchResult/Search';

                classScope.addElement("SearchTerm", searchTerm, form);
                classScope.addElement("TeachingYears", teachingYear, form);
                filters.forEach(f => classScope.addElement(f.type.toString(), f.key, form));

                form.submit();
            });
        }
    }

    private setFrameworksSearchResultsLink(searchTerm: string, filters: IFilterItem[]) {
        const anchor = document.getElementById("frameworksSearchResultsLink") as HTMLAnchorElement;

        if (anchor != null) {
            anchor.href = "#";
            const classScope = this;

            anchor.addEventListener("click", function () {
                const form = document.getElementById("breadcrumbSubmit") as HTMLFormElement;
                form.action = '/FrameworkSearchResult/Search';
                classScope.addElement("SearchTerm", searchTerm, form);

                filters.forEach(f => classScope.addElement(f.type.toString(), f.key, form));
                form.submit();
            });
        }
    }

    private addElement(name: string, value: string, form: HTMLFormElement) {
        const element = <HTMLInputElement>(document.createElement('input'));
        Object.assign(element, { name, value });
        form.appendChild(element);
    }
}