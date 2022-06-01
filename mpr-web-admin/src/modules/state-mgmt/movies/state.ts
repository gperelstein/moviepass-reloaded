import { IPagination } from "../../models/general";
import { IMovie } from "../../models/movie";

interface IMoviesListState{
    moviesList: IPagination<IMovie>,
    loading: boolean
}

export interface IState{
    movieList: IMoviesListState,
}
  
export const initialState: IState = {
    movieList: {
        moviesList:{      
            pageNumber: 0,
            pageSize: 0,
            totalResults: 0,
            items: [],
        },
        loading: false
    },
};