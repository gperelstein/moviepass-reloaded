import React, { memo, useCallback, useMemo } from 'react';

import Button from '@material-ui/core/Button';
import TableCell from '@material-ui/core/TableCell';
import TableRow from '@material-ui/core/TableRow';
import withStyles from '@material-ui/core/styles/withStyles';
import { listGlobalStyles, listTableRowStyles } from '../../../../../assets/styles';
import { getConditionalDefaultValue } from '../../../../../utils/generalUtils';
import ControlledButton from '../../../shared/ControlledButton';
import { IMovieTmdb } from '../../../../models/movieTmdb';
import { useNavigate, useParams } from 'react-router';
import { ROUTES } from '../../../../../constants';

export interface ITmdbGenreRowProps {
  movie: IMovieTmdb
}

const StyledTableRow = withStyles(listTableRowStyles)(TableRow);

const TmdbMovieRow = ({ movie }: ITmdbGenreRowProps) => {
  const listClasses = listGlobalStyles();

  let navigate = useNavigate();

  const handleClick = () => {
    navigate(`${ROUTES.CREATE_MOVIE.basePath}${movie.theMovieDbId}`);
  }
  
  return (
    <StyledTableRow data-testid="row-client-list-row" key={movie.theMovieDbId}>
      <TableCell className={listClasses.listName}>
        {movie.theMovieDbId}
      </TableCell>
      <TableCell className={listClasses.listName}>
        {movie.title}
      </TableCell>
      <TableCell className={listClasses.listName}>
        {movie.isInDatabase ? 'Yes' : 'No'}
      </TableCell>
      <TableCell className={listClasses.listName}>
        {!movie.isInDatabase ?
            <Button disableRipple={true} onClick={event => handleClick()}>
                Add
            </Button> : null}
      </TableCell>
    </StyledTableRow>
  );
};

export default TmdbMovieRow;