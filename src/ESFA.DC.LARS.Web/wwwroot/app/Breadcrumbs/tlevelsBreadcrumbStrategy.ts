import { IBreadcrumb } from '../Interfaces/IBreadcrumb';
import { IStorageItem } from '../Interfaces/IStorageItem';
import { IBreadcrumbStrategy } from '../Interfaces/IBreadcrumbStrategy';
import LinkService from '../Services/LinkService';
import { Page } from '../Enums/Page';

export class TLevelsBreadcrumbStrategy implements IBreadcrumbStrategy {

    public build(storageItem: IStorageItem): Array<IBreadcrumb> {
        const linkService = new LinkService();

        const breadcrumbs: Array<IBreadcrumb> = [
            { text: "Home", link: "/", page: Page.Home },
            { text: "Search Results", link: linkService.getTLevelsSearchResultsLink(storageItem), page: Page.Results },
        ];

        if (storageItem.page === Page.CommonComponent) {
            breadcrumbs.push({ text: 'Common components', link: '', page: Page.CommonComponent });
        } else {
            breadcrumbs.push({ text: storageItem.learningAimTitle, link: linkService.getQualificationsDetailsLinkWithoutYear(storageItem), page: Page.LearningAimDetails });
            breadcrumbs.push({ text: 'Category', link: '', page: Page.Category });
        }

        return breadcrumbs;
    }
}
