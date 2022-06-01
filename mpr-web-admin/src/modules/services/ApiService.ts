import axios, { AxiosResponse } from 'axios';
import { ENV } from '../../constants';
import { IPagination } from '../models/general';
import { userManager } from '../state-mgmt/auth/UserManager';
import { IMovieDetailsTmdb, IMovieTmdb } from '../models/movieTmdb';
import { IGenreTmdb } from '../models/genreTmdb';
import { IGenre, IGenrePost } from '../models/genre';
import { IMovie, IMoviePost } from '../models/movie';

interface IOptionRequest {
    method: string;
    body?: any;
    headers?: { [key: string]: any };
}

export class ApiService {

    private static apiUrl: string = ENV.API.URL;

    //The Movie Database
    public static async getTmdbMovies(pageNumber: number) : Promise<AxiosResponse<IPagination<IMovieTmdb>>>{
        return this.protectedRequest<IPagination<IMovieTmdb>>(`TheMovieDb/Movies?pageNumber=${pageNumber}`, {method: 'get'})
    }

    public static async getTmdbMovieDetails(tmdbMovieId: number) : Promise<AxiosResponse<IMovieDetailsTmdb>>{
        return this.protectedRequest<IMovieDetailsTmdb>(`TheMovieDb/Movie/${tmdbMovieId}`, {method: 'get'})
    }

    public static async getTmdbGenres() : Promise<AxiosResponse<IGenreTmdb[]>>{
        return this.protectedRequest<IGenreTmdb[]>('TheMovieDb/Genres', {method: 'get'})
    }

    //Genres
    public static async postGenre(genre : IGenrePost) : Promise<AxiosResponse<IGenre>>{
        return this.protectedRequest<IGenre>('Genres', {method: 'post', body: genre})
    }

    //Movies
    public static async getMovies(pageNumber: number, pageSize: number) : Promise<AxiosResponse<IPagination<IMovie>>>{
        return this.protectedRequest<IPagination<IMovie>>(`Movies?pageNumber=${pageNumber}&pageSize=${pageSize}`, {method: 'get'})
    }

    public static async postMovie(movie : IMoviePost) : Promise<AxiosResponse<IMovie>>{
        return this.protectedRequest<IMovie>('Movies', {method: 'post', body: movie})
    }

    public static async protectedRequest<T>(path: string, options: IOptionRequest): Promise<AxiosResponse<T>> {
        var user = await userManager.getUser();
        return this.createRequest<T>(path, options, user?.id_token as string);
    }

    private static createRequest<T>(path: string, options: IOptionRequest, token: string): Promise<AxiosResponse<T>> {

        options.headers = { ...options.headers, Authorization: `Bearer ${token}` };
    
        return axios({
            method: options.method,
            headers: {
                'Content-Type': 'application/json',
                Accept: 'application/json',
                ...options.headers,
            },
            data: options.body ? this.parseBody(options.body) : undefined,
            url: `${this.apiUrl}/${path}`,
            transformResponse: [
                (data) => {
                    if (typeof data === 'string') {
                        try {
                            data = JSON.parse(data);
                        } catch (e) { /* Ignore */
                        }
                    }
                    return data as T;
                }
            ]
        });
    }

    private static parseBody(body: { [key: string]: any }): string {
        try {
            return JSON.stringify(body);
        } catch {
            return '';
        }
    }
}