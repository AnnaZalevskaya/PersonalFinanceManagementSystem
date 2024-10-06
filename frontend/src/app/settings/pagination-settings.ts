export class PaginationSettings {
    private readonly maxPageSize = 50;
    private _pageSize = 5;
  
    pageNumber = 1;
  
    get pageSize(): number {
      return this._pageSize;
    }
  
    set pageSize(value: number) {
      this._pageSize = value > this.maxPageSize ? this.maxPageSize : value;
    }
}