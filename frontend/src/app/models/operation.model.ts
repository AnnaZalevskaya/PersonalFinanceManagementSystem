export interface Operation {
  id: string;
  accountId: number;
  date: Date;
  description: { [key: string]: any };
}