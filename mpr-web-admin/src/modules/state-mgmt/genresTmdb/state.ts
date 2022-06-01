import { IPagination } from '../../models/general';
import { IGenreTmdb } from '../../models/genreTmdb';

export interface IState {
    genres: IGenreTmdb[]
    loading: boolean
}

export const initialState: IState = {
    genres: [],
    loading: false
};