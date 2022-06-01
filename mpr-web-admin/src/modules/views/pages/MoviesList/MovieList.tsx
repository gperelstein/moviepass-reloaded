import { Table, TableBody, TableCell, TableHead, TableRow } from "@material-ui/core";
import { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { tableGlobalStyles } from "../../../../assets/styles/Tables/styles";
import { IPagination } from "../../../models/general";
import { IMovie } from "../../../models/movie";
import { ApiService } from "../../../services/ApiService";
import { FetchMoviesStartAction, FetchMoviesSuccessAction } from "../../../state-mgmt/movies/actions";
import { IRootState } from "../../../state-mgmt/rootState";
import Loader from "../../shared/Loader";
import PageTitle from "../../shared/PageTitle";
import Pagination from "../../shared/Pagination";
import MovieListRow from "./components/MovieListRow";

export interface IMovieListProps {
    loading: boolean,
    movies: IMovie[],
    fetchMoviesStart: () => FetchMoviesStartAction,
    fetchMoviesSuccess: (payload: IPagination<IMovie>) => FetchMoviesSuccessAction
}

const MovieList = ({ loading, movies, fetchMoviesStart, fetchMoviesSuccess } : IMovieListProps) => {

    const tableGlobalClasses = tableGlobalStyles();
    const [pageNumber, setPageNumber] = useState(1);
    const state = useSelector((state: IRootState) => state.movies);

    const onPageChange = (newPage : number) => {
        setPageNumber(newPage);
    }
    
    useEffect(() => {
        async function callApi(){
            fetchMoviesStart();
            var response = await ApiService.getMovies(pageNumber, 15);
            var data = response.data;
            console.log(typeof(data));
            fetchMoviesSuccess(data);
            console.log(movies);
            console.log(typeof(movies));
        }
        callApi()
    }, [pageNumber]);
    return(
        <>
            <PageTitle
                title="Movies"
            />
            <div className={tableGlobalClasses.tableWrapper}>
                {(!loading) ? (
                    <>
                        <Table aria-label="company-list">
                            <TableHead>
                                <TableRow>
                                    <TableCell>Title</TableCell>
                                    <TableCell>Language</TableCell>
                                    <TableCell>Duration</TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {state.movieList.moviesList.items.map(movie => (
                                    <MovieListRow key={movie.id} movie={movie} />
                                ))}
                            </TableBody>
                        </Table>
                        <Pagination page={pageNumber} count={Math.ceil(state.movieList.moviesList.totalResults / state.movieList.moviesList.pageSize)} onChange={onPageChange} />
                    </>
                ) : (
                    <Loader />
                )}
            </div>
        </>
    );
}

export default MovieList;