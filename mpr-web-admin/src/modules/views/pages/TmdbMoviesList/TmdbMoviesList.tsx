import React, { memo, useState, useCallback, useEffect, useMemo, useRef } from 'react';
import { Link } from 'react-router-dom';

import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Button from '@material-ui/core/Button';
import { listGlobalStyles } from '../../../../assets/styles';
import { IGenreTmdb } from '../../../models/genreTmdb';
import { FetchTmdbMoviesStartAction, FetchTmdbMoviesSuccessAction } from '../../../state-mgmt/moviesTmdb/actions';
import { ApiService } from '../../../services/ApiService';
import { useSelector } from 'react-redux';
import { IRootState } from '../../../state-mgmt/rootState';
import { tableGlobalStyles } from '../../../../assets/styles/Tables/styles';
import PageTitle from '../../shared/PageTitle';
import Loader from '../../shared/Loader';
import TmdbMovieRow from './components/TmdbMovieRow';
import { IMovieTmdb } from '../../../models/movieTmdb';
import { IPagination } from '../../../models/general';
import Pagination from '../../shared/Pagination';

export interface IMoviesListProps {
  loading: boolean,
  movies: IMovieTmdb[],
  fetchTmdbMoviesStart: () => FetchTmdbMoviesStartAction,
  fetchTmdbMoviesSuccess: (payload: IPagination<IMovieTmdb>) => FetchTmdbMoviesSuccessAction
}

const TmdbMoviesList = ({
  loading,
  movies,
  fetchTmdbMoviesStart,
  fetchTmdbMoviesSuccess
}: IMoviesListProps) => {

  const listClasses = listGlobalStyles();
  const clientListRef = useRef();
  const tableGlobalClasses = tableGlobalStyles();
  const state = useSelector((state: IRootState) => state.tmdbMovies);
  const [pageNumber, setPageNumber] = useState(1);

  const onPageChange = (newPage : number) => {
    setPageNumber(newPage);
  }

  useEffect(() => {
    async function callApi(){
      fetchTmdbMoviesStart();
      var response = await ApiService.getTmdbMovies(pageNumber);
      var data = response.data;
      console.log(typeof(data)); 
      fetchTmdbMoviesSuccess(data);
      console.log(movies);
      console.log(typeof(movies));     
    }
    callApi()
  }, [pageNumber]);

  return (
    <>
      <div className={tableGlobalClasses.tableWrapper}>
          {(!loading && movies.length > 0) ? (
            <>
              <Table aria-label="company-list">
                <TableHead>
                  <TableRow>
                    <TableCell>TheMovieDbId</TableCell>
                    <TableCell>Name</TableCell>
                    <TableCell>Is in database</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {Array.isArray(movies) ? movies.map(movie => (
                    <TmdbMovieRow key={movie.theMovieDbId} movie={movie} />
                  )) : null}
                </TableBody>
              </Table>
              <Pagination page={pageNumber} count={state.movieList.moviesList.totalResults} onChange={onPageChange} />
            </>
          ) : (
            <Loader />
          )}
        </div>
    </>
  );
};

export default TmdbMoviesList;