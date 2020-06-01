import { IBreadcrumb } from '../Interfaces/IBreadcrumb';
import { IStorageItem } from '../Interfaces/IStorageItem';
import { IBreadcrumbStrategy } from '../Interfaces/IBreadcrumbStrategy';
import LinkService from '../Services/LinkService';
import { Page } from '../Enums/Page';

export class UnitsBreadcrumbStrategy implements IBreadcrumbStrategy {

    public build(storageItem: IStorageItem): Array<IBreadcrumb> {
        const linkService = new LinkService();

        let breadcrumbs: Array<IBreadcrumb> = [
            { text: "Home", link: "/", page: Page.Home },
            { text: "Search Results", link: linkService.getUnitsSearchResultsLink(storageItem), page: Page.Results },
            { text: storageItem.learningAimTitle, link: linkService.getUnitsDetailsLink(storageItem), page: Page.LearningAimDetails },
            { text: "Category", link: "", page: Page.Category }
        ];
        return breadcrumbs;
    };
}
