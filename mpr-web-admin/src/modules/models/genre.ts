export interface IGenrePost{
    name: string,
    theMovieDbId: number | null
}

export interface IGenre extends IGenrePost{
    id: string
}