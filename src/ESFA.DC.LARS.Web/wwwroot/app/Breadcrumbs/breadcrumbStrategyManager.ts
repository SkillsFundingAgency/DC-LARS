import { SearchType } from "../Enums/SearchType";
import { IStorageItem } from "../Interfaces/IStorageItem";
import { IBreadcrumb } from "../Interfaces/IBreadcrumb";
import { IBreadcrumbStrategy } from "../Interfaces/IBreadcrumbStrategy";
import { QualificationsBreadcrumbStrategy } from "./qualificationsBreadcrumbStrategy";
import { DefaultBreadcrumbStrategy } from "./defaultBreadcrumbStrategy";
import { UnitsBreadcrumbStrategy } from "./unitsBreadcrumbStrategy";
import { FrameworksBreadcrumbStrategy } from "./frameworksBreadcrumbStrategy";
import { StandardsBreadcrumbStrategy } from "./standardsBreadcrumbStrategy";
import { TLevelsBreadcrumbStrategy } from "./tlevelsBreadcrumbStrategy";

class BreadcrumbStrategyManager {

    private _breadcrumbStrategy: IBreadcrumbStrategy = new DefaultBreadcrumbStrategy();

    private _breadcrumbStrategies = {
        [SearchType.Qualifications]: new QualificationsBreadcrumbStrategy(),
        [SearchType.Units]: new UnitsBreadcrumbStrategy(),
        [SearchType.Frameworks]: new FrameworksBreadcrumbStrategy(),
        [SearchType.Standards]: new StandardsBreadcrumbStrategy(),
        [SearchType.TLevels]: new TLevelsBreadcrumbStrategy()
    };

    public setBreadcrumbStrategy(searchType: SearchType) {
        let selectedStrategy = this._breadcrumbStrategies[searchType];
        if (!selectedStrategy) {
            selectedStrategy = new DefaultBreadcrumbStrategy();
        }
        this._breadcrumbStrategy = selectedStrategy;
    }

    public build(storeageItem: IStorageItem): Array<IBreadcrumb> {
        return this._breadcrumbStrategy.build(storeageItem);
    }
}

export const breadcrumbStrategyManager = new BreadcrumbStrategyManager();