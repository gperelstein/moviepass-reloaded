import { Button, Grid, Link, TextField } from "@material-ui/core";
import { IMovieDetailsTmdb } from "../../../models/movieTmdb";
import ControlledInput from "../../shared/ControlledInput";
import { inputGlobalStyles } from '../../../../assets/styles/Inputs/styles';
import { EditTmdbMovieDetailsAction, FetchTmdbMovieDetailsStartAction, FetchTmdbMovieDetailsSuccessAction } from "../../../state-mgmt/moviesTmdb/actions";
import { useSearchParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router";
import { ApiService } from "../../../services/ApiService";
import Loader from "../../shared/Loader";
import Card from "../../shared/Card";
import PageTitle from "../../shared/PageTitle";
import Image from 'material-ui-image'
import { height, width } from "@mui/system";
import { getLanguage } from "../../../../utils/generalUtils";
import { IGenreTmdb } from "../../../models/genreTmdb";
import EditIcon from '@mui/icons-material/Edit';
import { useStyles } from "./styles";
import { IMoviePost } from "../../../models/movie";
import { ROUTES } from "../../../../constants";

interface IMovieCreationFormProps{
    movie: IMovieDetailsTmdb,
    loading: boolean,
    fetchTmdbMovieDetailsStart: () => FetchTmdbMovieDetailsStartAction,
    fetchTmdbMovieDetailsSuccess: (payload: IMovieDetailsTmdb) => FetchTmdbMovieDetailsSuccessAction,
    editTmdbMovieDetails: (payload: IMovieDetailsTmdb) => EditTmdbMovieDetailsAction,
}

const MovieCreationForm = ({ movie, loading, fetchTmdbMovieDetailsStart, fetchTmdbMovieDetailsSuccess, editTmdbMovieDetails }: IMovieCreationFormProps) => {

    const [editTrailer, setEditTrailer] = useState<boolean>(false);
    const inputGlobalClasses = inputGlobalStyles();
    const classes = useStyles();
    const {id} = useParams();
    let navigate = useNavigate();

    const genresToString = (genres: IGenreTmdb[]) : string => {
        let result = '';
        for(let i = 0; i < genres.length; i++){
            if(i < genres.length - 1){
                result = result.concat(`${genres[i].name}, `);
                continue;
            }
            result = result.concat(`${genres[i].name}`);
        }

        return result;
    }

    const handleClick = async (event : React.FormEvent<HTMLButtonElement>) => {
        event.preventDefault();
        const newMovie : IMoviePost = {
            theMovieDbId: movie.theMovieDbId,
            title: movie.title,
            language: movie.language,
            poster: movie.poster,
            overview: movie.overview,
            duration: movie.duration,
            tagLine: movie.tagLine,
            trailer: movie.trailer,
            genreNames: movie.genres.map(x => x.name),
        }
        await ApiService.postMovie(newMovie);
        navigate(ROUTES.TMDB.path);
    }

    const handleEditTrailer = (event : React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        event.preventDefault();
        let movieAux = {
            ...movie,
            [event.target.name as keyof typeof movie]: event.target.value
        }
        editTmdbMovieDetails(movieAux);
    }

    useEffect(() => {
        async function callApi(){
            fetchTmdbMovieDetailsStart();
            var response = await ApiService.getTmdbMovieDetails(parseInt(id as string, 10));
            var data = response.data;
            fetchTmdbMovieDetailsSuccess(data);    
          }
          callApi()  
    }, []);

    return(
        <>
            {!loading ?
                <>
                    <PageTitle
                        title="Movie Creation Form"
                    />
                    <Card title="Movie Details">
                        <Grid container={true} spacing={4} alignItems={'center'} >
                            <Grid container={true} item={true} xs={8} spacing={4} alignItems={'center'}>
                                <Grid item={true} xs={8}>
                                    <ControlledInput label="Title" styleClass={`${inputGlobalClasses.inputPaddingBottom}`}>
                                        <TextField
                                            autoComplete="off"
                                            variant="outlined"
                                            placeholder="Title"
                                            type="text"
                                            fullWidth={true}
                                            name="serialNumber"
                                            value={movie.title}
                                            required={true}
                                            inputProps={{
                                                'data-testid': 'serialNumber',
                                                maxLength: 25,
                                            }}
                                            disabled={true}
                                        />
                                    </ControlledInput>
                                </Grid>
                                <Grid item={true} xs={8}>
                                    <ControlledInput label="Overview" styleClass={` ${classes.descriptionInput}  ${inputGlobalClasses.inputPaddingBottom}`}>
                                        <TextField
                                            autoComplete="off"
                                            variant="outlined"
                                            placeholder="Overview"
                                            type="text"
                                            fullWidth={true}
                                            name="serialNumber"
                                            value={movie.overview}
                                            required={true}
                                            inputProps={{
                                                'data-testid': 'serialNumber',
                                                maxLength: 25,
                                            }}
                                            disabled={true}
                                            minRows={3}
                                            multiline={true}
                                        />
                                    </ControlledInput>
                                </Grid>                            
                            </Grid>                        
                            <Grid item={true} xs={4}>
                                <Image
                                    src={movie.poster}
                                    cover={false}
                                    imageStyle={{width: 'auto', height: '100%'}}
                                />
                            </Grid>
                        </Grid>
                        <Grid container={true} spacing={4}>
                            <Grid item={true} xs={6}>
                                <ControlledInput label="Original language" styleClass={`${inputGlobalClasses.inputPaddingBottom}`}>
                                    <TextField
                                        autoComplete="off"
                                        variant="outlined"
                                        placeholder="Original language"
                                        type="text"
                                        fullWidth={true}
                                        name="serialNumber"
                                        value={getLanguage(movie.language)}
                                        required={true}
                                        inputProps={{
                                            'data-testid': 'serialNumber',
                                            maxLength: 25,
                                        }}
                                        disabled={true}
                                    />
                                </ControlledInput>
                            </Grid>
                            <Grid item={true} xs={6}>
                                <ControlledInput label="Tag Line" styleClass={`${inputGlobalClasses.inputPaddingBottom}`}>
                                    <TextField
                                        autoComplete="off"
                                        variant="outlined"
                                        placeholder="Tag Line"
                                        type="text"
                                        fullWidth={true}
                                        name="serialNumber"
                                        value={movie.tagLine}
                                        required={true}
                                        inputProps={{
                                            'data-testid': 'serialNumber',
                                            maxLength: 25,
                                        }}
                                        disabled={true}
                                    />
                                </ControlledInput>
                            </Grid>
                        </Grid>
                        <Grid container={true} spacing={4}>
                            <Grid item={true} xs={6}>
                                <ControlledInput label="Duration in minutes" styleClass={`${inputGlobalClasses.inputPaddingBottom}`}>
                                    <TextField
                                        autoComplete="off"
                                        variant="outlined"
                                        placeholder="Duration in minutes"
                                        type="text"
                                        fullWidth={true}
                                        name="serialNumber"
                                        value={movie.duration}
                                        required={true}
                                        inputProps={{
                                            'data-testid': 'serialNumber',
                                            maxLength: 50,
                                        }}
                                        disabled={true}
                                    />
                                </ControlledInput>
                            </Grid>
                            <Grid item={true} xs={6}>
                                <ControlledInput label="Genres" styleClass={`${inputGlobalClasses.inputPaddingBottom}`}>
                                    <TextField
                                        autoComplete="off"
                                        variant="outlined"
                                        placeholder="Tag Line"
                                        type="text"
                                        fullWidth={true}
                                        name="serialNumber"
                                        value={genresToString(movie.genres)}
                                        required={true}
                                        inputProps={{
                                            'data-testid': 'serialNumber',
                                            maxLength: 25,
                                        }}
                                        disabled={true}
                                    />
                                </ControlledInput>
                            </Grid>
                        </Grid>
                        <Grid container={true} spacing={4}>
                            <Grid item={true} xs={6}>
                                <ControlledInput label="Trailer" styleClass={`${inputGlobalClasses.inputPaddingBottom}`}>
                                    <TextField
                                        autoComplete="off"
                                        variant="outlined"
                                        placeholder="Trailer"
                                        type="text"
                                        fullWidth={true}
                                        name="trailer"
                                        value={movie.trailer}
                                        required={true}
                                        inputProps={{
                                            'data-testid': 'serialNumber',
                                            maxLength: 25,
                                        }}
                                        onChange={e => handleEditTrailer(e)}
                                    />
                                </ControlledInput>
                            </Grid>
                        </Grid>
                        <Button
                            color="secondary"
                            variant="contained"
                            size="large"
                            type="submit"
                            onClick={event => handleClick(event)}
                        >
                            Add Movie
                        </Button>
                    </Card>                                                 
                </>
            : <Loader />}
        </>
    );
}

export default MovieCreationForm;