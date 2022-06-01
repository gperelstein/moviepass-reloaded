import React, { memo } from 'react';
import PageTitle from '../../shared/PageTitle';
import { useStyles } from './styles';
import CardAdmin from '../../shared/CardAdmin';
import { MoviesListIcon, TheMovieDbIcon, ROUTES, GenresListIcon } from '../../../../constants';
import { Grid } from '@material-ui/core';

const Movies = () => {
  const classes = useStyles();
  return (
    <div className={classes.container}>
      <PageTitle title="Movies Administration" styles={{ marginTop: -8 }} />
      <div className={classes.cardsContainer}>
        <Grid container={true} spacing={10} alignItems={'center'} >
          <Grid item={true} xl={3} md={4} xs={5}>            
            <CardAdmin
              title={"Movies"}
              bodyFirstLine={"List of movies avalilable"}
              bodySecondLine={"in MoviePass"}
              logo={<MoviesListIcon width={40} />}
              linkDescription={"List of movies"}
              pathLink={ROUTES.MOVIESLIST.path}
            />
          </Grid>
          <Grid item={true} xl={3} md={4} xs={5}>                   
            <CardAdmin
              title={"Genres"}
              bodyFirstLine={"List of genres avalilable"}
              bodySecondLine={"in MoviePass"}
              logo={<GenresListIcon width={10000}/>}
              linkDescription={"List of genres"}
              pathLink={ROUTES.MOVIESLIST.path}
            /> 
          </Grid>
        </Grid>
        <Grid container={true} spacing={4} alignItems={'center'} >
          <Grid item={true} xs={6}>               
            <CardAdmin
              title={"TheMovieDb"}
              bodyFirstLine={"List of movies avalilable"}
              bodySecondLine={"in TheMovieDb"}
              logo={<TheMovieDbIcon width={100}/>}
              linkDescription={"List of movies"}
              pathLink={ROUTES.TMDB.path}
            /> 
          </Grid>
        </Grid>
      </div>
    </div>
  );
};

export default Movies;