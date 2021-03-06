﻿import { FrameworksBreadcrumbStrategy } from "../Breadcrumbs/frameworksBreadcrumbStrategy";
import { emptyIStorageItem } from "../Interfaces/IStorageItem";
import { SearchType } from "../Enums/SearchType";
import { Page } from "../Enums/Page";

describe('Path specfic breadcrumbs should ignore pages from other paths', () => {

    const storageItem = Object.assign(emptyIStorageItem(), { searchType: SearchType.Frameworks});

    const sut = new FrameworksBreadcrumbStrategy();

    test("Category page breadcrumb should not contain component page", () => {
        storageItem.page = Page.Category;
        const result = sut.build(storageItem);
        expect(result.some(r => r.page === Page.CommonComponent)).toBe(false);
    });

    test("Component page breadcrumb should not contain category page", () => {
        storageItem.page = Page.CommonComponent;
        const result = sut.build(storageItem);
        expect(result.some(r => r.page === Page.Category)).toBe(false);
    });

});