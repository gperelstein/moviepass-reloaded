import { connect } from "react-redux";
import { Dispatch } from "redux";
import { IPagination } from "../../../models/general";
import { IGenreTmdb } from "../../../models/genreTmdb";
import { IMovieDetailsTmdb, IMovieTmdb } from "../../../models/movieTmdb";
import { actions, TmdbMovieAction } from "../../../state-mgmt/moviesTmdb/actions";
import { IRootState } from "../../../state-mgmt/rootState";
import MovieCreationForm from "./MovieCreationForm";

export const mapStateToProps = (state: IRootState) => ({
    loading: state.tmdbMovies.movieSelected.loading,
    movie: state.tmdbMovies.movieSelected.movieDetails
});

export const mapDispatchToProps = (dispatch: Dispatch<TmdbMovieAction>) => ({
    fetchTmdbMovieDetailsStart: () => dispatch(actions.fetchTmdbMovieDetailsStart()),
    fetchTmdbMovieDetailsSuccess: (payload : IMovieDetailsTmdb) => dispatch(actions.fetchTmdbMovieDetailsSuccess(payload)),
    editTmdbMovieDetails: (payload : IMovieDetailsTmdb) => dispatch(actions.editTmdbMovieDetails(payload)),
});

export default connect(mapStateToProps, mapDispatchToProps)(MovieCreationForm);