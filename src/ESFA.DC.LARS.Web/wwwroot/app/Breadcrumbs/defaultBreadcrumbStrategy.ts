import { IBreadcrumb } from '../Interfaces/IBreadcrumb';
import { IStorageItem } from '../Interfaces/IStorageItem';
import { IBreadcrumbStrategy } from '../Interfaces/IBreadcrumbStrategy';
import { Page } from '../Enums/Page';

export class DefaultBreadcrumbStrategy implements IBreadcrumbStrategy {

    public build(storageItem: IStorageItem): Array<IBreadcrumb> {
        let breadcrumbs: Array<IBreadcrumb> = [
            { text: "Start a new search", link: "/", page: Page.Home },
        ];

        return breadcrumbs;
    };
}
