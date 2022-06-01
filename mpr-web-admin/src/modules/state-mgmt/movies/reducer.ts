import { ActionType, MovieAction } from './actions';
import { initialState, IState } from './state';

export const reducer = (state: IState = initialState, { type, payload }: MovieAction): IState => {
  switch (type) {
    case ActionType.FETCH_MOVIES_START:
      return {
        ...state,
        movieList: {
          ...state.movieList,
          loading: true
        }
      };
    case ActionType.FETCH_MOVIES_SUCCESS:
      return {
        ...state,
        movieList: {
          ...state.movieList,          
          moviesList: payload,
          loading: false,
        }        
      };
    default:
      return state;
  }
};