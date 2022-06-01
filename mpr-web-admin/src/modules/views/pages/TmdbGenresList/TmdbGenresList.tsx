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
import { FetchTmdbGenresStartAction, FetchTmdbGenresSuccessAction } from '../../../state-mgmt/genresTmdb/actions';
import { ApiService } from '../../../services/ApiService';
import { useSelector } from 'react-redux';
import { IRootState } from '../../../state-mgmt/rootState';
import { tableGlobalStyles } from '../../../../assets/styles/Tables/styles';
import TmdbGenreRow from './components/TmdbGenreRow';
import PageTitle from '../../shared/PageTitle';
import Loader from '../../shared/Loader';

export interface IGenresListProps {
  loading: boolean,
  genres: IGenreTmdb[],
  fetchTmdbGenresStart: () => FetchTmdbGenresStartAction,
  fetchTmdbGenresSuccess: (payload: IGenreTmdb[]) => FetchTmdbGenresSuccessAction
}

const TmdbGenresList = ({
  loading,
  genres,
  fetchTmdbGenresStart,
  fetchTmdbGenresSuccess
}: IGenresListProps) => {

  const listClasses = listGlobalStyles();
  const clientListRef = useRef();
  const tableGlobalClasses = tableGlobalStyles();
  const state = useSelector((state: IRootState) => state.tmdbGenres);

  useEffect(() => {
    async function callApi(){
      fetchTmdbGenresStart();
      var response = await ApiService.getTmdbGenres();
      var data = response.data;
      console.log(typeof(data)); 
      fetchTmdbGenresSuccess(data);
      console.log(genres);
      console.log(typeof(genres));     
    }
    callApi()
  }, [fetchTmdbGenresStart]);

  return (
    <>
      <div className={tableGlobalClasses.tableWrapper}>
          {(!loading && genres.length > 0) ? (
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
                  {genres.map(genre => (
                    <TmdbGenreRow
                      key={genre.theMovieDbId}
                      genre={genre}
                      fetchTmdbGenresStart={fetchTmdbGenresStart}
                      fetchTmdbGenresSuccess={fetchTmdbGenresSuccess}
                    />
                  ))}
                </TableBody>
              </Table>
            </>
          ) : (
            <Loader />
          )}
        </div>
    </>
  );
};

export default TmdbGenresList;