import { IBreadcrumb } from '../Interfaces/IBreadcrumb';
import { IStorageItem } from '../Interfaces/IStorageItem';
import { IBreadcrumbBuilder } from '../Interfaces/IBreadcrumbBuilder';
import { Page } from '../Enums/Page';

export class DefaultBreadcrumbBuilder implements IBreadcrumbBuilder {

    public build(storageItem: IStorageItem): Array<IBreadcrumb> {
        let breadcrumbs: Array<IBreadcrumb> = [
            { text: "Start a new search", link: "/", page: Page.Home },
        ];

        return breadcrumbs;
    };
}
