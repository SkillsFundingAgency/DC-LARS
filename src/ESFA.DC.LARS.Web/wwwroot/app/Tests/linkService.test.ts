import LinkService from '../Services/LinkService';
import { emptyIStorageItem } from "../Interfaces/IStorageItem";
import { FilterType } from '../Enums/FilterType';

describe('Link service should use current academic year if no teaching year filter provided', () => {

    const storageItem = Object.assign(emptyIStorageItem(), { currentAcademicYear: '1819'});

    const sut = new LinkService();

    test("Units search results link should contain current academic year", () => {
        const result = sut.getUnitsSearchResultsLink(storageItem);
        expect(result).toContain(storageItem.currentAcademicYear);
    });

    test("Qualifications search results link should contain current academic year", () => {
        const result = sut.getQualificationsSearchResultsLink(storageItem);
        expect(result).toContain(storageItem.currentAcademicYear);
    });
});

describe('Link service should use teaching year filter when provided', () => {

    const filterDate = '1920';
    const storageItem = Object.assign(emptyIStorageItem(), {
        currentAcademicYear: '1819', filters:
            [{ key: filterDate, value: '', type: FilterType.TeachingYears }]
    });

    const sut = new LinkService();

    test("Units search results link should contain teaching filter year from filter", () => {
        const result = sut.getUnitsSearchResultsLink(storageItem);
        expect(result).toContain(filterDate);
    });

    test("Qualifications search results link should contain teaching filter year from filter", () => {
        const result = sut.getQualificationsSearchResultsLink(storageItem);
        expect(result).toContain(filterDate);
    });
});

describe('Link service should add hasFilters param if search filters applied', () => {
    const storageItem = Object.assign(emptyIStorageItem(), {
        currentAcademicYear: '1819', filters:
            [{ key: "test", value: '', type: FilterType.AwardingBodies }]
    });

    const sut = new LinkService();

    test("Units search results link should contain hasFilters true", () => {
        const result = sut.getUnitsSearchResultsLink(storageItem);
        expect(result).toContain("&hasFilters=true");
    });

    test("Qualifications search Results link should contain hasFilters true", () => {
        const result = sut.getQualificationsSearchResultsLink(storageItem);
        expect(result).toContain("&hasFilters=true");
    });

    test("Frameworks search results link should contain hasFilters true", () => {
        const result = sut.getFrameworksSearchResultsLink(storageItem);
        expect(result).toContain("&hasFilters=true");
    });
});

