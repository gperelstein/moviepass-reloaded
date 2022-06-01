import { ActionType, TmdbMovieAction } from './actions';
import { initialState, IState } from './state';

export const reducer = (state: IState = initialState, { type, payload }: TmdbMovieAction): IState => {
  switch (type) {
    case ActionType.FETCH_TMDB_MOVIES_START:
      return {
        ...state,
        movieList: {
          ...state.movieList,
          loading: true
        }
      };
    case ActionType.FETCH_TMDB_MOVIES_SUCCESS:
      return {
        ...state,
        movieList: {
          ...state.movieList,          
          moviesList: payload,
          loading: false,
        }        
      };
    case ActionType.FETCH_TMDB_MOVIEDETAILS_START:
      return {
        ...state,
        movieSelected: {
          ...state.movieSelected,
          loading: true
        }
      };
    case ActionType.FETCH_TMDB_MOVIEDETAILS_SUCCESS:
      return {
        ...state,
        movieSelected: {
          ...state.movieSelected,
          movieDetails: payload,
          loading: false
        }
      };
    case ActionType.EDIT_TMDB_MOVIE:
      return {
        ...state,
        movieSelected: {
          ...state.movieSelected,          
          movieDetails: payload,
          loading: false,
        }        
      };
    default:
      return state;
  }
};