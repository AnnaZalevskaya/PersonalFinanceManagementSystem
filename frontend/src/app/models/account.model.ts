import { AccountType } from "./account-type.model";
import { Currency } from "./currency.model";
import { Operation } from "./operation.model";
import { User } from "./user.model";

export interface Account {
    id: number;
    name: string;
    balance: number;
    accountTypeId: number; 
    type: AccountType;
    currencyId: number;
    currency: Currency;
    user: User;
    operations: Operation[];
}





