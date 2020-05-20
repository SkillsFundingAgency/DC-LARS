import { FrameworksBreadcrumbBuilder } from "../Breadcrumbs/frameworksBreadcrumbBuilder";
import { IStorageItem } from "../Interfaces/IStorageItem";
import { SearchType } from "../Enums/SearchType";
import { Page } from "../Enums/Page";

describe('Path specfic breadcrumbs should ignore pages from other paths', () => {

    const storageItem: IStorageItem = {
        searchTerm: '', learnAimRef: '', learningAimTitle: '',
        currentAcademicYear: '', searchType: SearchType.Frameworks, frameworkCode: '', programType: '', pathwayCode: '', filters: []
    };

    const sut = new FrameworksBreadcrumbBuilder();

    test("Category page breadcrumb should not contain component page", () => {
        const result = sut.build(storageItem, Page.Category);
        expect(result.some(r => r.page === Page.CommonComponent)).toBe(false);
    });

    test("Component page breadcrumb should not contain category page", () => {
        const result = sut.build(storageItem, Page.CommonComponent);
        expect(result.some(r => r.page === Page.Category)).toBe(false);
    });

});