import { AccountType } from "./account-type.model";
import { Currency } from "./currency.model";

export interface AccountAction {
    name: string;
    accountTypeId: number; 
    type: AccountType;
    currencyId: number;
    currency: Currency;
    userId: number;
}