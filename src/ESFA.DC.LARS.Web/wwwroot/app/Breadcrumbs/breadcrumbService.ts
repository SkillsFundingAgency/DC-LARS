import { IBreadcrumbBuilder } from '../Interfaces/IBreadcrumbBuilder';
import { QualificationsBreadcrumbBuilder } from './qualificationsBreadcrumbBuilder';
import { UnitsBreadcrumbBuilder } from './unitsBreadcrumbBuilder';
import { FrameworksBreadcrumbBuilder } from './frameworksBreadcrumbBuilder';
import { IStorageItem } from '../Interfaces/IStorageItem';
import { Page } from '../Enums/Page';
import { SearchType } from '../Enums/SearchType';
import { IBreadcrumb } from '../Interfaces/IBreadcrumb';

export class BreadcrumbService {

    private linkTemplate: string = "<li class='govuk-breadcrumbs__list-item'><a class='govuk-breadcrumbs__link' href='{{link}}'>{{title}}</a></li>";
    private textTemplate: string = "<li class='govuk-breadcrumbs__list-item'>{{title}}</li>";

    public buildBreadcrumb(storageItem: IStorageItem, page: Page): void {
        const breadcrumbs = this.breadcrumbsFactory(storageItem.searchType);

        const element = document.getElementById("breadCrumbs") as HTMLElement;
        element.innerHTML = this.createHTML(breadcrumbs.build(storageItem, page), page);
    }

    private breadcrumbsFactory(searchType: SearchType): IBreadcrumbBuilder {
        if (searchType === SearchType.Qualifications) {
            return new QualificationsBreadcrumbBuilder();
        }

        if (searchType === SearchType.Units) {
            return new UnitsBreadcrumbBuilder();
        }

        return new FrameworksBreadcrumbBuilder();
    };

    private createHTML(breadcrumbs: Array<IBreadcrumb>, page: Page) {
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
