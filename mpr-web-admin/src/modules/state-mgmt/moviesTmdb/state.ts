import { IPagination } from '../../models/general';
import { IMovieDetailsTmdb, IMovieTmdb } from '../../models/movieTmdb';

interface IMoviesTmdbListState{
  moviesList: IPagination<IMovieTmdb>,
  loading: boolean
}

interface IMovieDetailsTmdbState{
  movieDetails: IMovieDetailsTmdb
  loading: boolean
}

export interface IState{
  movieList: IMoviesTmdbListState,
  movieSelected: IMovieDetailsTmdbState
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
  movieSelected: {
    movieDetails:{
      theMovieDbId: 0,
      title: '',
      language: '',
      poster: '',
      overview: '',
      isInDatabase: false,
      duration: 0,
      tagLine: '',
      trailer: '',
      genres: [],
    },
    loading: false
  }
};