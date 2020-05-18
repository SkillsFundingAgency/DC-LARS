import { IStorageItem } from '../Interfaces/IStorageItem';
import StorageService from './storageService';
import { IFilterItem, FilterType } from '../Interfaces/IFilterItem';
import { formHelper } from '../Helpers/formHelper';
import { INameValue } from '../Interfaces/INameValue';
import { constants } from '../constants';
import { SearchType } from '../SearchType';
import { filterStoreService } from './filterStoreService';
import { enumHelper } from '../Helpers/enumHelper';

export default class LinkService {

    private storageService: StorageService;

    constructor() {
        this.storageService = new StorageService(sessionStorage);
    }

    public setLinks(): void {
        const storageItem = this.storageService.retrieve(constants.storageKey) as IStorageItem;
        this.renderBreadcrumbs(storageItem);
        this.setAnchorLinkById("homeLink", "/");
        this.setAnchorLinkById("learningAimDetailLink", this.getDetailsLinkForType(storageItem));
        this.setAnchorLinkById("frameworksLink", `/Frameworks/${storageItem.learnAimRef}`);
        this.setAnchorLinkById("pathwaysLink", `/FrameworkDetails/${storageItem.frameworkCode}/${storageItem.programType}/${storageItem.pathwayCode}`);
        this.setLearningAimDetailText(storageItem.learningAimTitle);
    }

    private getDetailsLinkForType(storageItem : IStorageItem) {
        if (storageItem.searchType === SearchType.Qualifications) {
            return `/LearningAimDetails/${storageItem.learnAimRef}?academicYear=${this.getTeachingYear(storageItem)}`;
        }

        if (storageItem.searchType === SearchType.Units) {
            return `/UnitDetails/${storageItem.learnAimRef}?academicYear=${this.getTeachingYear(storageItem)}`;
        }
        return '';
    }

    public redirectToResults(serverSearchType: string, oldSearchResults: SearchType): void {
        const linkService = new LinkService();
        const clientSearchType = enumHelper.ConvertServerEnumValueToClientEnum(serverSearchType);
        let updatedFilters: Array<IFilterItem> = [];

        if (clientSearchType === SearchType.Frameworks) {
            window.location.href = linkService.getFrameworksSearchResultsLink();
        }
        else {
            if (clientSearchType === SearchType.Units) {
                window.location.href = linkService.getUnitsSearchResultsLink();
            }

            if (clientSearchType === SearchType.Qualifications) {
                window.location.href = linkService.getLearningAimSearchResultsLink();
            }

            // If moving to a search that has teaching years then keep exisiting teaching year
            // filter or use current academic year if not present.
            const filters = filterStoreService.getSavedFilters(oldSearchResults);
            if (filters.some(f => f.type === FilterType.TeachingYears)) {
                updatedFilters = filters.filter(f => f.type === FilterType.TeachingYears);
            } else {
                const storageItem = this.storageService.retrieve(constants.storageKey) as IStorageItem;
                updatedFilters.push({ type: FilterType.TeachingYears, key: storageItem.currentAcademicYear, value: '' });
            }
        }

        this.storageService.updateFilters(constants.storageKey, updatedFilters);
    }

    private getLearningAimSearchResultsLink(): string {
        const storageItem = this.storageService.retrieve(constants.storageKey) as IStorageItem;
        return `/LearningAimSearchResult?SearchTerm=${storageItem.searchTerm}&TeachingYear=${this.getTeachingYear(storageItem)}`;
    }

    private getUnitsSearchResultsLink(): string {
        const storageItem = this.storageService.retrieve(constants.storageKey) as IStorageItem;
        return `/UnitSearchResult?SearchTerm=${storageItem.searchTerm}&TeachingYear=${this.getTeachingYear(storageItem)}`;
    }

    private getFrameworksSearchResultsLink(): string {
        const storageItem = this.storageService.retrieve(constants.storageKey) as IStorageItem;
        return `/FrameworkSearchResult?SearchTerm=${storageItem.searchTerm}`;
    }

    private getTeachingYear(storageItem: IStorageItem): string {
        const teachingFilter = storageItem.filters.find(f => f.type === FilterType.TeachingYears);
        if (teachingFilter) {
            return teachingFilter.key;
        }
        return storageItem.currentAcademicYear;
    }

    private renderBreadcrumbs(storageItem: IStorageItem) {
        const breadcrumbs = this.getBreadcrumbsForSearchType(storageItem.searchType);

        if (breadcrumbs) {
            breadcrumbs.removeAttribute("style");
            if (storageItem.searchType === SearchType.Frameworks) {
                this.setFrameworksSearchResultsLink(storageItem.searchTerm, storageItem.filters);
            }
            if (storageItem.searchType === SearchType.Qualifications) {
                this.setLearningAimSearchResultsLink(storageItem.searchTerm, this.getTeachingYear(storageItem), storageItem.filters, '/LearningAimSearchResult/Search');
            }

            if (storageItem.searchType === SearchType.Units) {
                this.setLearningAimSearchResultsLink(storageItem.searchTerm, this.getTeachingYear(storageItem), storageItem.filters, '/UnitSearchResult/Search');
            }
        }
    }

    private getBreadcrumbsForSearchType(searchType: SearchType): HTMLElement {
        const resultsBreadcrumbs = document.getElementById("resultsBreadcrumb") as HTMLElement;
        const frameworkBreadcrumbs = document.getElementById("frameworksBreadcrumbs") as HTMLElement;
        const learningAimBreadcrumbs = document.getElementById("learningAimBreadcrumbs") as HTMLElement;

        if (resultsBreadcrumbs) {
            return resultsBreadcrumbs;
        }

        if (searchType === SearchType.Frameworks) {
            this.removeElement(learningAimBreadcrumbs);
            return frameworkBreadcrumbs;
        }

        this.removeElement(frameworkBreadcrumbs);
        return learningAimBreadcrumbs
    }

    private removeElement(element: HTMLElement) {
        if (element) {
            const parent = element.parentNode as Node;
            parent.removeChild(element);
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

    private setLearningAimSearchResultsLink(searchTerm: string, teachingYear: string, filters : IFilterItem[], baseUrl: string) {
        const anchor = document.getElementById("searchResultsLink") as HTMLAnchorElement;

        if (anchor) {
            anchor.href = "#";
            const formElements = this.getSearchAndFilterElements(searchTerm, filters);
            // If no teaching year filter applied then use default.
            if (!formElements.find(f => f.name === FilterType.TeachingYears)) {
                formElements.push({ name: FilterType.TeachingYears, value: teachingYear });
            }
            this.addAnchorSubmitEvent(anchor, baseUrl, formElements);
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