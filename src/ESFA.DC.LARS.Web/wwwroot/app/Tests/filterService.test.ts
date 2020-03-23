import { filterService } from '../Services/filterService';
import { IFilterItem, FilterType } from '../Interfaces/IFilterItem';

it('removeFilterFromArray should remove filter item from array of filter items', () => {

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