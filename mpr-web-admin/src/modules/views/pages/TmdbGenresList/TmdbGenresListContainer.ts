import { connect } from "react-redux";
import { Dispatch } from "redux";
import { IGenreTmdb } from "../../../models/genreTmdb";
import { actions, ActionType, FetchTmdbGenresStartAction, TmdbGenreAction } from "../../../state-mgmt/genresTmdb/actions";
import { IRootState } from "../../../state-mgmt/rootState";
import GenresList from "./TmdbGenresList";

export const mapStateToProps = (state: IRootState) => ({
    loading: state.tmdbGenres.loading,
    genres: state.tmdbGenres.genres
});

export const mapDispatchToProps = (dispatch: Dispatch<TmdbGenreAction>) => ({
    fetchTmdbGenresStart: () => dispatch(actions.fetchTmdbGenresStart()),
    fetchTmdbGenresSuccess: (payload : IGenreTmdb[]) => dispatch(actions.fetchTmdbGenresSuccess(payload))
});

export default connect(mapStateToProps, mapDispatchToProps)(GenresList);