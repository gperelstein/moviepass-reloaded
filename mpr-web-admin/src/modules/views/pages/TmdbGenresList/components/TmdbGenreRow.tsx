import React, { memo, useCallback, useMemo } from 'react';

import Button from '@material-ui/core/Button';
import TableCell from '@material-ui/core/TableCell';
import TableRow from '@material-ui/core/TableRow';
import withStyles from '@material-ui/core/styles/withStyles';
import { listGlobalStyles, listTableRowStyles } from '../../../../../assets/styles';
import { getConditionalDefaultValue } from '../../../../../utils/generalUtils';
import ControlledButton from '../../../shared/ControlledButton';
import { IGenreTmdb } from '../../../../models/genreTmdb';
import { ApiService } from '../../../../services/ApiService';
import { IGenrePost } from '../../../../models/genre';
import { FetchTmdbGenresStartAction, FetchTmdbGenresSuccessAction } from '../../../../state-mgmt/genresTmdb/actions';

export interface ITmdbGenreRowProps {
  genre: IGenreTmdb,
  fetchTmdbGenresStart: () => FetchTmdbGenresStartAction,
  fetchTmdbGenresSuccess: (payload: IGenreTmdb[]) => FetchTmdbGenresSuccessAction
}

const StyledTableRow = withStyles(listTableRowStyles)(TableRow);

const TmdbGenreRow = ({ genre, fetchTmdbGenresStart, fetchTmdbGenresSuccess }: ITmdbGenreRowProps) => {
  
  const listClasses = listGlobalStyles();

  const handleClickAddButton = async () => {
    await ApiService.postGenre(genre as IGenrePost);
    fetchTmdbGenresStart();
    var response = await ApiService.getTmdbGenres();
    var data = response.data;
    console.log(typeof(data)); 
    fetchTmdbGenresSuccess(data);
  }

  return (
    <StyledTableRow data-testid="row-client-list-row" key={genre.theMovieDbId}>
      <TableCell className={listClasses.listName}>
        {genre.theMovieDbId}
      </TableCell>
      <TableCell className={listClasses.listName}>
        {genre.name}
      </TableCell>
      <TableCell className={listClasses.listName}>
        {genre.isInDatabase ? 'Yes' : 'No'}
      </TableCell>
      <TableCell className={listClasses.listName}>
        {!genre.isInDatabase ?
            <Button disableRipple={true} onClick={e => handleClickAddButton()}>
                Add
            </Button> : null}
      </TableCell>
    </StyledTableRow>
  );
};

export default TmdbGenreRow;