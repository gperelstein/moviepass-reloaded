import { IState as ITmdbMoviesState } from './moviesTmdb/state';
import { IState as ITmdbGenresState } from './genresTmdb/state';
import { IState as IMoviesState } from './movies/state';

export interface IRootState {
  tmdbMovies: ITmdbMoviesState,
  tmdbGenres: ITmdbGenresState,
  movies: IMoviesState,
}
