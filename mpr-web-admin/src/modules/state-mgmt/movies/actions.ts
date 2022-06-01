import { IAction, IPagination } from "../../models/general";
import { IMovie } from "../../models/movie";

export enum ActionType {
    FETCH_MOVIES_START = '[movies] fetch tmdb movies start',
    FETCH_MOVIES_SUCCESS = '[movies] fetch tmdb movies success',
}

export interface FetchMoviesStartAction extends IAction<ActionType.FETCH_MOVIES_START, {}>{}
export interface FetchMoviesSuccessAction extends IAction<ActionType.FETCH_MOVIES_SUCCESS, IPagination<IMovie>>{}

export const actions = {
    fetchMoviesStart: () : FetchMoviesStartAction => ({ type: ActionType.FETCH_MOVIES_START, payload: {} }),
    fetchMoviesSuccess: (payload : IPagination<IMovie>) : FetchMoviesSuccessAction => ({ type: ActionType.FETCH_MOVIES_SUCCESS, payload: payload }),
}

export type MovieAction = FetchMoviesStartAction | FetchMoviesSuccessAction;