import { IBreadcrumb } from '../Interfaces/IBreadcrumb';
import { IStorageItem } from '../Interfaces/IStorageItem';
import { IBreadcrumbBuilder } from '../Interfaces/IBreadcrumbBuilder';
import LinkService from '../Services/LinkService';
import { Page } from '../Enums/Page';

export class StandardsBreadcrumbBuilder implements IBreadcrumbBuilder {

    public build(storageItem: IStorageItem): Array<IBreadcrumb> {
        const linkService = new LinkService();

        let breadcrumbs: Array<IBreadcrumb> = [
            { text: "Home", link: "/", page: Page.Home },
            { text: "Search Results", link: linkService.getStandardsSearchResultsLink(storageItem), page: Page.Results },
            { text: storageItem.standardName, link: linkService.getStandardsDetailsLink(storageItem), page: Page.Standard },
            { text: 'Common components', link: '', page: Page.CommonComponent }
        ];
        return breadcrumbs;
    };
}
