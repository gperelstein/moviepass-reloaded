import { connect } from "react-redux";
import { Dispatch } from "redux";
import { IPagination } from "../../../models/general";
import { IGenreTmdb } from "../../../models/genreTmdb";
import { IMovieTmdb } from "../../../models/movieTmdb";
import { actions, TmdbMovieAction } from "../../../state-mgmt/moviesTmdb/actions";
import { IRootState } from "../../../state-mgmt/rootState";
import TmdbMoviesList from "./TmdbMoviesList";

export const mapStateToProps = (state: IRootState) => ({
    loading: state.tmdbMovies.movieList.loading,
    movies: state.tmdbMovies.movieList.moviesList.items
});

export const mapDispatchToProps = (dispatch: Dispatch<TmdbMovieAction>) => ({
    fetchTmdbMoviesStart: () => dispatch(actions.fetchTmdbMoviesStart()),
    fetchTmdbMoviesSuccess: (payload : IPagination<IMovieTmdb>) => dispatch(actions.fetchTmdbMoviesSuccess(payload))
});

export default connect(mapStateToProps, mapDispatchToProps)(TmdbMoviesList);