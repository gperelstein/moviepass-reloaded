import { IGenreTmdb } from "./genreTmdb"

export interface IMovieTmdb{
    theMovieDbId: number,
    title: string,
    language: string,
    poster: string,
    overview: string,
    isInDatabase: boolean
}

export interface IMovieDetailsTmdb extends IMovieTmdb{
    duration: number,
    tagLine: string,
    trailer: string,
    genres: IGenreTmdb[]
}