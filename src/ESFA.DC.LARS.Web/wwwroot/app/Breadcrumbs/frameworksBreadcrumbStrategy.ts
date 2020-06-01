import { IBreadcrumb } from '../Interfaces/IBreadcrumb';
import { IStorageItem } from '../Interfaces/IStorageItem';
import { IBreadcrumbStrategy } from '../Interfaces/IBreadcrumbStrategy';
import LinkService from '../Services/LinkService';
import { Page } from '../Enums/Page';

export class FrameworksBreadcrumbStrategy implements IBreadcrumbStrategy {

    public build(storageItem: IStorageItem): Array<IBreadcrumb> {
        const linkService = new LinkService();

        let breadcrumbs: Array<IBreadcrumb> = [
            { text: "Home", link: "/", page: Page.Home },
            { text: "Search Results", link: linkService.getFrameworksSearchResultsLink(storageItem), page: Page.Results },
            { text: 'Pathways', link: linkService.getFrameworksDetailsLink(storageItem), page: Page.Pathway }
        ];

        if (storageItem.page === Page.CommonComponent) {
            breadcrumbs.push({ text: 'Common components', link: '', page: Page.CommonComponent });
        } else {
            breadcrumbs.push({ text: storageItem.learningAimTitle, link: linkService.getQualificationsDetailsLinkForFrameworks(storageItem), page: Page.LearningAimDetails });
            breadcrumbs.push({ text: 'Category', link: '', page: Page.Category });
        }

        return breadcrumbs;
    };
}
