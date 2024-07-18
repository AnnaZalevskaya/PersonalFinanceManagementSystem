import { Time } from "@angular/common";

export interface RecurringOperation {
  id: string;
  accountId: string;
  userId: string;
  name: string;
  amount: number;
  executionTime: Time; 
  intervalType: number;
  interval: string;
  startDate: string; 
  endDate: Date; 
}

export interface RecurringOperationAction {
  accountId: string;
  userId: string;
  name: string;
  amount: number;
  executionTime: Time; 
  interval: number;
  startDate: string; 
  endDate: Date; 
}
  
export enum IntervalEnum {
  Daily = 1,
  Weekly = 2,
  OnceEveryTwoWeek = 3,
  Monthly = 4,
  Yearly = 5
}