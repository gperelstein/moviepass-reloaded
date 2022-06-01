import { combineReducers } from "redux";
import { reducer as TmdbMovieReducer } from "./moviesTmdb/reducer";
import { reducer as TmdbGenreReducer } from "./genresTmdb/reducer";
import { reducer as MovieReducer } from "./movies/reducer"


const reducers = combineReducers({
    tmdbMovies: TmdbMovieReducer,
    tmdbGenres: TmdbGenreReducer,
    movies: MovieReducer
})

export default reducers;

export type RootState = ReturnType<typeof reducers>