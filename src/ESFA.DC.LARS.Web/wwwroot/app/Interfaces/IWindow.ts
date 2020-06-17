import { IGOVUKFrontend } from "./IGOVUKFrontend";

declare global {
    interface Window {
        GOVUKFrontend: IGOVUKFrontend
    }
}
