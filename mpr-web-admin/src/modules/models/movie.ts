import { IGenre } from "./genre"

interface IMovieBase{
    title: string,
    language: string,
    poster: string,
    overview: string,
    duration: number,
    tagLine: string,
    trailer: string,
}

export interface IMoviePost extends IMovieBase{
    theMovieDbId: number,    
    genreNames: string[]
}

export interface IMovie extends IMovieBase{
    id: string,
    genres: IGenre[]
}