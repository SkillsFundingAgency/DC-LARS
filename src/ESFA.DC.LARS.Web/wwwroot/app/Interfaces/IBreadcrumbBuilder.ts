import { IStorageItem } from "./IStorageItem";
import { IBreadcrumb } from './IBreadcrumb';

export interface IBreadcrumbBuilder {
    build(storeageItem: IStorageItem): Array<IBreadcrumb>;
}