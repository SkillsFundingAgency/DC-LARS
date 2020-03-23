import { filterService } from '../Services/filterService';
import { IFilterItem, FilterType } from '../Interfaces/IFilterItem';

test('removeFilterFromArray should remove filter item from array of filter items', () => {

    const filters: Array<IFilterItem> = [
        {
            key: 'foo',
            value: 'bar',
            type: FilterType.AwardingBody
        },
        {
            key: 'keep',
            value: 'this',
            type: FilterType.AwardingBody
        }];

    const filterToRemove: IFilterItem = {
        key: 'foo',
        value: 'bar',
        type: FilterType.AwardingBody
    };

    const result = filterService.removeFilterFromArray(filters, filterToRemove);

    expect(result.length).toBe(1);
    expect(result[0].key).toBe('keep');
});

test('sortFilters should order filters by type then by value', () => {

    const filters: Array<IFilterItem> = [
        {
            key: 'test1',
            value: 'third',
            type: FilterType.TeachingYears
        },
        {
            key: 'test2',
            value: 'first',
            type: FilterType.AwardingBody
        },
        {
            key: 'test3',
            value: 'second',
            type: FilterType.TeachingYears
        }];

    const result = filterService.sortFilters(filters);

    expect(result[0].value).toBe('first');
    expect(result[1].value).toBe('second');
    expect(result[2].value).toBe('third');
});