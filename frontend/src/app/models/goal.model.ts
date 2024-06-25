export interface Goal {
  id: number;
  accountId: number;
  name: string;
  typeId: number;
  type: string;
  startDate: Date;
  endDate: Date;
  amount: number;
  progress: number;
}

export interface ActionGoal {
  accountId: string;
  name: string;
  typeId: number;
  startDate: Date;
  endDate: Date;
  amount: number;
}

export enum GoalType {
  Save = 1,
  Spend = 2,
  Reach = 3
}