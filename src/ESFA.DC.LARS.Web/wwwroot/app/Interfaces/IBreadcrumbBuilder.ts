import { IStorageItem } from "./IStorageItem";
import { Page } from '../Enums/Page';
import { IBreadcrumb } from './IBreadcrumb';

export interface IBreadcrumbBuilder {
    build(storeageItem: IStorageItem, page: Page): Array<IBreadcrumb>;
}