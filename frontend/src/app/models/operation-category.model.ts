import { OperationCategoryType } from "./operation-category-type.model";

export interface OperationCategory {
    id: number;
    name: string;
    type: OperationCategoryType;
}