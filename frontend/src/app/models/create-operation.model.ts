import { Account } from "./account.model";

export interface CreateOperation {
    accountId: number;
    account: Account;
    description: { [key: string]: any };
}