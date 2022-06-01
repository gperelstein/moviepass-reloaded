import { ActionType, TmdbGenreAction } from './actions';
import { initialState, IState } from './state';

export const reducer = (state: IState = initialState, { type, payload }: TmdbGenreAction): IState => {
  switch (type) {
    case ActionType.FETCH_TMDB_GENRES_START:
      return {
        ...state,
        loading: true
      };
    case ActionType.FETCH_TMDB_GENRES_SUCCESS:
      return {
        ...state,
        genres: payload,
        loading: false,
        
      };
    default:
      return state;
  }
};