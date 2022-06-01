import Button from '@material-ui/core/Button';
import TableCell from '@material-ui/core/TableCell';
import TableRow from '@material-ui/core/TableRow';
import withStyles from '@material-ui/core/styles/withStyles';
import { listGlobalStyles, listTableRowStyles } from '../../../../../assets/styles';
import { useNavigate, useParams } from 'react-router';
import { ROUTES } from '../../../../../constants';
import { IMovie } from '../../../../models/movie';
import { getLanguage } from '../../../../../utils/generalUtils';

export interface IMovieListRowProps {
  movie: IMovie
}

const StyledTableRow = withStyles(listTableRowStyles)(TableRow);

const MovieListRow = ({ movie }: IMovieListRowProps) => {
  const listClasses = listGlobalStyles();

  let navigate = useNavigate();
  
  return (
    <StyledTableRow data-testid="row-client-list-row" key={movie.id}>
      <TableCell className={listClasses.listName}>
        {movie.title}
      </TableCell>
      <TableCell className={listClasses.listName}>
        {getLanguage(movie.language)}
      </TableCell>
      <TableCell className={listClasses.listName}>
        {movie.duration}
      </TableCell>
    </StyledTableRow>
  );
};

export default MovieListRow;