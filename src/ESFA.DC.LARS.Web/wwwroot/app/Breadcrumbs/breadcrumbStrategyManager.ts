import { SearchType } from "../Enums/SearchType";
import { IStorageItem } from "../Interfaces/IStorageItem";
import { IBreadcrumb } from "../Interfaces/IBreadcrumb";
import { IBreadcrumbStrategy } from "../Interfaces/IBreadcrumbStrategy";
import { QualificationsBreadcrumbStrategy } from "./qualificationsBreadcrumbStrategy";
import { DefaultBreadcrumbStrategy } from "./defaultBreadcrumbStrategy";
import { UnitsBreadcrumbStrategy } from "./unitsBreadcrumbStrategy";
import { FrameworksBreadcrumbStrategy } from "./frameworksBreadcrumbStrategy";
import { StandardsBreadcrumbStrategy } from "./standardsBreadcrumbStrategy";

class BreadcrumbStrategyManager {

    private _breadcrumbStrategy: IBreadcrumbStrategy = new DefaultBreadcrumbStrategy();

    private _breadcrumbBuilders = {
        [SearchType.Qualifications]: new QualificationsBreadcrumbStrategy(),
        [SearchType.Units]: new UnitsBreadcrumbStrategy(),
        [SearchType.Standards]: new FrameworksBreadcrumbStrategy(),
        [SearchType.Frameworks]: new StandardsBreadcrumbStrategy()
    };

    public setBreadcrumbStrategy(searchType: SearchType) {
        let selectedStrategy = this._breadcrumbBuilders[searchType];
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