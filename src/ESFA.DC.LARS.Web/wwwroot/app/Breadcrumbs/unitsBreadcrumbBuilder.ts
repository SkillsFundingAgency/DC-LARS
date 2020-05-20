import { IBreadcrumb } from '../Interfaces/IBreadcrumb';
import { IStorageItem } from '../Interfaces/IStorageItem';
import { IBreadcrumbBuilder } from '../Interfaces/IBreadcrumbBuilder';
import LinkService from '../Services/LinkService';
import { Page } from '../Enums/Page';

export class UnitsBreadcrumbBuilder implements IBreadcrumbBuilder {

    public build(storageItem: IStorageItem, page: Page): Array<IBreadcrumb> {
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
