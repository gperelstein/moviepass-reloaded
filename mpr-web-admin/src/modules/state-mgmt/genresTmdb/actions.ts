import { IAction } from "../../models/general";
import { Dispatch } from "redux";
import { IGenreTmdb } from "../../models/genreTmdb";

export enum ActionType {
  FETCH_TMDB_GENRES_START = '[tmdb] fetch tmdb genres start',
  FETCH_TMDB_GENRES_SUCCESS = '[tmdb] fetch tmdb genres success'
}

export interface FetchTmdbGenresStartAction extends IAction<ActionType.FETCH_TMDB_GENRES_START, {}>{}
export interface FetchTmdbGenresSuccessAction extends IAction<ActionType.FETCH_TMDB_GENRES_SUCCESS, IGenreTmdb[]>{}

const fetchTmdbGenresStart = () => {
  return (dispatch: Dispatch<FetchTmdbGenresStartAction>) => {
    dispatch({
        type: ActionType.FETCH_TMDB_GENRES_START,
        payload: {}
    });
  }
}

const fetchTmdbGenresSuccess = (payload : IGenreTmdb[]) => {
  return (dispatch: Dispatch<FetchTmdbGenresSuccessAction>) => {
    dispatch({
        type: ActionType.FETCH_TMDB_GENRES_SUCCESS,
        payload: payload
    });
  }
}

export const actions = {
  fetchTmdbGenresStart: () : FetchTmdbGenresStartAction => ({ type: ActionType.FETCH_TMDB_GENRES_START, payload: {} }),
  fetchTmdbGenresSuccess: (payload : IGenreTmdb[]) : FetchTmdbGenresSuccessAction=> ({ type: ActionType.FETCH_TMDB_GENRES_SUCCESS, payload: payload })
}

export type TmdbGenreAction = FetchTmdbGenresStartAction | FetchTmdbGenresSuccessAction;