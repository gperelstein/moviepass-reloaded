export interface IPagination<T> {
    items: T[];
    pageNumber: number;
    totalResults: number;
    pageSize: number;
}

export interface IAction<T, E>{
    type: T,
    payload: E
}