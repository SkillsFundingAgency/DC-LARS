import { IStorageItem } from '../Interfaces/IStorageItem';
import { Page } from '../Enums/Page';
import { IBreadcrumb } from '../Interfaces/IBreadcrumb';
import { breadcrumbStrategyManager } from './breadcrumbStrategyManager';

export class BreadcrumbService {

    private linkTemplate: string = "<li class='govuk-breadcrumbs__list-item'><a class='govuk-breadcrumbs__link' href='{{link}}'>{{title}}</a></li>";
    private textTemplate: string = "<li class='govuk-breadcrumbs__list-item'>{{title}}</li>";

    public buildBreadcrumb(storageItem: IStorageItem): void {
        breadcrumbStrategyManager.setBreadcrumbStrategy(storageItem.searchType);
        const element = document.getElementById("breadCrumbs") as HTMLElement;
        element.innerHTML = this.createHTML(breadcrumbStrategyManager.build(storageItem), storageItem.page);
    }

    private createHTML(breadcrumbs: Array<IBreadcrumb>, page: Page): string {
        let breadcrumbHTML = "";

        breadcrumbs.every((breadcrumb: IBreadcrumb) => {
            if (breadcrumb.page === page) {
                breadcrumbHTML += this.textTemplate.replace("{{title}}", breadcrumb.text);
            } else {
                breadcrumbHTML += this.linkTemplate.replace("{{title}}", breadcrumb.text).replace("{{link}}", breadcrumb.link);
            }
            return breadcrumb.page !== page;
        });
        return breadcrumbHTML;
    }
}
