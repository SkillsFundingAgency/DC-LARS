import { AbstractFilterStoreStrategy } from "./abstractFilterStoreStrategy";
import { qualificationsFilterStoreStrategy } from "./qualificationsfilterStoreStrategy";
import { standardsFilterStoreStrategy } from "./standardsFilterStoreStrategy";
import { frameworksFilterStoreStrategy } from "./frameworksFilterStoreStrategy";
import { unitsFilterStoreStrategy } from "./unitsFilterStoreStrategy";
import { SearchType } from "../Enums/SearchType";
import { tlevelsFilterStoreStrategy } from "./tlevelsFilterStoreStrategy";

class FilterStoreStrategyManager {
    private _filterStoreStrategies = {
        [SearchType.Qualifications]: qualificationsFilterStoreStrategy,
        [SearchType.Units]: unitsFilterStoreStrategy,
        [SearchType.Frameworks]: frameworksFilterStoreStrategy,
        [SearchType.Standards]: standardsFilterStoreStrategy,
        [SearchType.TLevels]: tlevelsFilterStoreStrategy
    };

    public getFilterStoreStrategy(searchType: SearchType): AbstractFilterStoreStrategy {
        return this._filterStoreStrategies[searchType];
    }
}

export const filterStoreStrategyManager = new FilterStoreStrategyManager();
