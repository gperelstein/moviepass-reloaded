import { connect } from "react-redux";
import { Dispatch } from "redux";
import { IPagination } from "../../../models/general";
import { IMovie } from "../../../models/movie";
import { actions, MovieAction } from "../../../state-mgmt/movies/actions";
import { IRootState } from "../../../state-mgmt/rootState";
import MovieList from "./MovieList";

export const mapStateToProps = (state: IRootState) => ({
    loading: state.movies.movieList.loading,
    movies: state.movies.movieList.moviesList.items
});

export const mapDispatchToProps = (dispatch: Dispatch<MovieAction>) => ({
    fetchMoviesStart: () => dispatch(actions.fetchMoviesStart()),
    fetchMoviesSuccess: (payload : IPagination<IMovie>) => dispatch(actions.fetchMoviesSuccess(payload))
});

export default connect(mapStateToProps, mapDispatchToProps)(MovieList);