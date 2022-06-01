import { Provider } from 'react-redux';
import { Routes, Route } from 'react-router';
import { store } from './state-mgmt/store';
import { BrowserRouter } from 'react-router-dom';
import { ThemeProvider } from '@material-ui/core/styles';
import ProtectedRoute from './views/shared/ProtectedRoute/ProtectedRoute';
import CssBaseline from '@material-ui/core/CssBaseline';
import { theme } from '../constants/style';
import { ROUTES } from '../constants/routes';
import Landing from './views/pages/Landing';
import Login from './views/pages/Login';
import Protected from './views/pages/Protected';
import SignInCallback from './views/pages/SignInCallback';
import Movies from './views/pages/Movies';
import Dashboard from './views/pages/Dashboard';
import TheMovieDb from './views/pages/TheMovieDb';
import MovieCreationForm from './views/pages/MovieCreationForm';
import MovieListContainer from './views/pages/MoviesList/MovieListContainer';

export default function App() {
  return (
    <Provider store={store}>
      <ThemeProvider theme={theme}>
          <CssBaseline />
          <BrowserRouter>
          <Routes>
            <Route path={ROUTES.CALLBACK.path} element={<SignInCallback />} />            
            <Route path={ROUTES.LANDING.path} element={<Landing />} />
            <Route path={"/"} element={<ProtectedRoute/>}>
              <Route path={ROUTES.DASHBOARD.path} element={<Dashboard />} />              
              <Route path={ROUTES.MOVIES.path} element={<Movies />} />
              <Route path={ROUTES.TMDB.path} element={<TheMovieDb />} />              
              <Route path={ROUTES.CREATE_MOVIE.path} element={<MovieCreationForm />} />
              <Route path={ROUTES.MOVIESLIST.path} element={<MovieListContainer />} />
            </Route>
            <Route path={ROUTES.PROTECTED.path} element={<Protected />} />
            <Route path={ROUTES.LOGIN.path} element={<Login />} />
          </Routes>
          </BrowserRouter>
        </ThemeProvider>
    </Provider>
  );
}
