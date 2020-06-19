import { IBreadcrumb } from '../Interfaces/IBreadcrumb';
import { IStorageItem } from '../Interfaces/IStorageItem';
import { IBreadcrumbStrategy } from '../Interfaces/IBreadcrumbStrategy';
import LinkService from '../Services/LinkService';
import { Page } from '../Enums/Page';

export class StandardsBreadcrumbStrategy implements IBreadcrumbStrategy {

    public build(storageItem: IStorageItem): Array<IBreadcrumb> {
        const linkService = new LinkService();

        const breadcrumbs: Array<IBreadcrumb> = [
            { text: "Home", link: "/", page: Page.Home },
            { text: "Search Results", link: linkService.getStandardsSearchResultsLink(storageItem), page: Page.Results },
            { text: storageItem.standardName, link: linkService.getStandardsDetailsLink(storageItem), page: Page.Standard }
        ];

        if (storageItem.page === Page.CommonComponent) {
            breadcrumbs.push({ text: 'Common components', link: '', page: Page.CommonComponent});
        } else {
            breadcrumbs.push({ text: 'Related learning aims', link: linkService.getStandardsRelatedAimsLink(storageItem), page: Page.RelatedLearningAims});
            breadcrumbs.push({ text: storageItem.learningAimTitle, link: linkService.getQualificationsDetailsLinkWithoutYear(storageItem), page: Page.LearningAimDetails });
            breadcrumbs.push({ text: 'Category', link: '', page: Page.Category });
        }

        return breadcrumbs;
    }

}
