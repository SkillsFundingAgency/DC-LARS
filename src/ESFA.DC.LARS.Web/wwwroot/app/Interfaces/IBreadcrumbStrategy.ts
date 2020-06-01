import { IStorageItem } from "./IStorageItem";
import { IBreadcrumb } from './IBreadcrumb';

export interface IBreadcrumbStrategy {
    build(storeageItem: IStorageItem): Array<IBreadcrumb>;
}