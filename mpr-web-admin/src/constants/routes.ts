import { Create } from "@mui/icons-material";

export const ROUTES = {
  LANDING: { path: '/' },
  LOGIN: { path: '/login' },
  DASHBOARD: { path: '/dashboard' },
  MOVIES: { path: '/movies' },
  PROTECTED: { path: '/protected' },
  CALLBACK: { path: '/callback' },
  MOVIESLIST: { path: '/movieslist' },
  TMDB: { path: '/themoviedb' },
  CREATE_MOVIE: { path: '/themoviedb/movies/:id', basePath: '/themoviedb/movies/' },
  CINEMAS: { path: '/cinemas' },
};