import { IAction } from "../../models/general";
import { IMovieDetailsTmdb, IMovieTmdb } from "../../models/movieTmdb";
import { IPagination } from "../../models/general";

export enum ActionType {
  FETCH_TMDB_MOVIES_START = '[tmdb] fetch tmdb movies start',
  FETCH_TMDB_MOVIES_SUCCESS = '[tmdb] fetch tmdb movies success',
  FETCH_TMDB_MOVIEDETAILS_START = '[tmdb] fetch tmdb movie details start',
  FETCH_TMDB_MOVIEDETAILS_SUCCESS = '[tmdb] fetch tmdb movie details success',
  EDIT_TMDB_MOVIE = '[tmdb] edit tmdb information',
  ADD_MOVIE = '[tmdb] add movie',
}

export interface FetchTmdbMoviesStartAction extends IAction<ActionType.FETCH_TMDB_MOVIES_START, {}>{}
export interface FetchTmdbMoviesSuccessAction extends IAction<ActionType.FETCH_TMDB_MOVIES_SUCCESS, IPagination<IMovieTmdb>>{}
export interface FetchTmdbMovieDetailsStartAction extends IAction<ActionType.FETCH_TMDB_MOVIEDETAILS_START, {}>{}
export interface FetchTmdbMovieDetailsSuccessAction extends IAction<ActionType.FETCH_TMDB_MOVIEDETAILS_SUCCESS, IMovieDetailsTmdb>{}
export interface EditTmdbMovieDetailsAction extends IAction<ActionType.EDIT_TMDB_MOVIE, IMovieDetailsTmdb>{}

export const actions = {
  fetchTmdbMoviesStart: () : FetchTmdbMoviesStartAction => ({ type: ActionType.FETCH_TMDB_MOVIES_START, payload: {} }),
  fetchTmdbMoviesSuccess: (payload : IPagination<IMovieTmdb>) : FetchTmdbMoviesSuccessAction=> ({ type: ActionType.FETCH_TMDB_MOVIES_SUCCESS, payload: payload }),
  fetchTmdbMovieDetailsStart: () : FetchTmdbMovieDetailsStartAction => ({ type: ActionType.FETCH_TMDB_MOVIEDETAILS_START, payload: {} }),
  fetchTmdbMovieDetailsSuccess: (payload : IMovieDetailsTmdb) : FetchTmdbMovieDetailsSuccessAction => ({ type: ActionType.FETCH_TMDB_MOVIEDETAILS_SUCCESS, payload: payload }),
  editTmdbMovieDetails: (payload : IMovieDetailsTmdb) : EditTmdbMovieDetailsAction => ({ type: ActionType.EDIT_TMDB_MOVIE, payload: payload }),
}

export type TmdbMovieAction = FetchTmdbMoviesStartAction | FetchTmdbMoviesSuccessAction | FetchTmdbMovieDetailsStartAction | FetchTmdbMovieDetailsSuccessAction | EditTmdbMovieDetailsAction;