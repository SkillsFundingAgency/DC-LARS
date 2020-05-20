import { IStorageItem } from "./IStorageItem";
import { Page } from '../Enums/Page';

export interface IBreadcrumb {
    text: string;
    link: string;
    page: Page
}