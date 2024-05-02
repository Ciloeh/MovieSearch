/* eslint-disable jsx-a11y/anchor-is-valid */
import React, { useCallback, useEffect, useState } from "react";
import "./AppHome.css";
import { Field, Label, SearchBox } from "@fluentui/react-components";
import { IMovies } from "../../Interface/Movies";
import axios, { AxiosError } from 'axios';
import { handleAxiosError, MOVIES_API } from "../../constants/Endpoint";
import { ILatestSearch } from "../../Interface/LatestSearched";
import { useNavigate } from "react-router-dom";
import {
    Button,
    Dialog,
    DialogSurface,
    DialogTitle,
    DialogBody,
    DialogContent,
    DialogActions,
    DialogTrigger,
    Input,
    makeStyles,
} from "@fluentui/react-components";
import type { DialogTriggerChildProps } from "@fluentui/react-components";
import { Inituser, IUser } from "../../Interface/Iusers";
import { UserContext } from "../Context/Statemanagement";

const useStyles = makeStyles({
    content: {
      display: "flex",
      flexDirection: "column",
      rowGap: "10px",
    },
  });

  interface Users {
    id: number;
    phone: string;
    name: string;
    dateAdded: Date;
    searchQueries:  ILatestSearch[];
  }
interface AppHomeProps {
    searchTerm: string;
  }

const AppHome: React.FC<AppHomeProps>= ({searchTerm}) => {
    const [movieTitle, setmovieTitle] = useState<any>("Explore Movies & TV shows");
    const [movies, setMovies] = useState<IMovies[]>([]);
    const [latestSearched, setLatestSearched] = useState<ILatestSearch[]>([]);
    const navigate = useNavigate();
    const [isOpen, setIsOpen] = useState(true);
    const styles = useStyles();
    const userContext = React.useContext(UserContext);
 
    console.log("User context", userContext?.user);
  
    const searchMoviesByTitle = useCallback(async (title: string): Promise<IMovies[] | undefined> => {
        try {
          const response = await axios.get<IMovies[]>(`${MOVIES_API}/Movies/search/${title}`);
          return response.data;
        } catch (error) {
          if (axios.isAxiosError(error)) {
            handleAxiosError(error);
          } else {
            console.error(`Unexpected error: ${error}`);
          }
        }
      }, []); 
      

      
    const getLatestSearched = useCallback(async (): Promise<ILatestSearch[] | undefined> => {
        try {
            const userid = userContext?.user?.id;
          const response = await axios.get<ILatestSearch[]>(`${MOVIES_API}/SearchQueries?id=${userid}`);
          return response.data;
        } catch (error) {
          if (axios.isAxiosError(error)) {
            handleAxiosError(error);
          } else {
            console.error(`Unexpected error: ${error}`);
          }
        }
      }, [userContext?.user?.id]); 
      
    


  

      const fetchMovies = useCallback(async (data: any) => {
        const result = await searchMoviesByTitle(data);
        if (result) {
          setMovies(result);
        }
      }, [searchMoviesByTitle]);
      
      const fetchLatestSearch = useCallback(async () => {
        const result = await getLatestSearched();
        if (result) {
            setLatestSearched(result);
        }
      }, [getLatestSearched]);
      

      const handleSearch = (event: any, data: any) => {
        if(data.value > 3){
            fetchMovies(searchTerm);
        }
        
      };

      const SaveSearchedMovie = useCallback(async (query: string): Promise<void> => {
        try {
            console.log("I got here");
          const userid = userContext?.user?.id;
          if (userid !== undefined) {
            await axios.post(`${MOVIES_API}/SearchQueries?query=${query}&userid=${userid}`);
          } else {
            console.error("User ID is undefined");
          }
        } catch (error) {
          if (axios.isAxiosError(error)) {
            handleAxiosError(error);
          } else {
            console.error(`Unexpected error: ${error}`);
          }
        }
      }, [userContext]);
      
      const handleMovieClick = (movieTitle:string) => {
         SaveSearchedMovie(movieTitle);
        navigate(`/single-view/${movieTitle}`);
      };

    useEffect(() => {
        if(userContext?.user?.id){
            fetchLatestSearch();
        }
        if (searchTerm) {
            fetchMovies(searchTerm);
        }
        console.log("latest search", latestSearched, "user: ", userContext?.user?.id);
      }, [fetchLatestSearch, fetchMovies, latestSearched, movies, searchTerm, userContext?.user?.id]);
    return (
        <>
         <div>
         <div className="Movie-search-landing__header mobile-disp">
       <h2 className="Moview-search-landing__title">Hi! Jude, Search your favorite movies</h2>
       <div className="Mview-search-header__search movie-search-header__search-page">
       <Label htmlFor="movie-search-header-form-input-box" id="movie-search-header-form-input-label" className="movie-search-header__form-input-label"> Search</Label>
        <div className="movie-search-header__search-input-wrapper">
        <Field className="">
        <SearchBox  placeholder="Search Movie.."  onChange={handleSearch} appearance="filled-darker" />
      </Field>
        </div>

       </div>
       </div>
       <div className="result-canvas-display loading-inner">
       <div className="movie-collection-page ">
            <div>
            <h1 className="movie-collection-page__title movie-typography-title-2-emphasized">
            Search history From your Watchlist
            </h1>
            <ul className="movie-collection-page__grid">
            {latestSearched.map((movie) => (
          <li key={movie.imdbID} className="movie-collection-page__item">
          <div className="movie-editorial-item-lockup" onClick={() => handleMovieClick(movie.title)}>
          <div className="movie-media-artwork-v2__container movie-media-artwork-v2__container-serve">
              <img src={`${movie.poster}`} className="movie-media-artwork-v2__image"  sizes="(min-width:300px) and (max-width:739px) 246px, (min-width:740px) and (max-width:999px) 333px, (min-width:1000px) and (max-width:1319px) 329px, 335px" alt={movie.title}/>
          </div>
          
          </div>
          <span className="movie-title">{movie.title}</span>
      </li>
      ))}
          


            </ul>
            </div>
        </div>
        <div className="movie-collection-page ">
            <div>
            <h1 className="movie-collection-page__title movie-typography-title-2-emphasized">
                {movieTitle}
            </h1>
            <ul className="movie-collection-page__grid">
            {movies.map((movie) => (
          <li key={movie.imdbID} className="movie-collection-page__item">
          <div className="movie-editorial-item-lockup" onClick={() => handleMovieClick(movie.title)}>
          <div className="movie-media-artwork-v2__container movie-media-artwork-v2__container-serve">
              <img src={`${movie.poster}`} className="movie-media-artwork-v2__image"  sizes="(min-width:300px) and (max-width:739px) 246px, (min-width:740px) and (max-width:999px) 333px, (min-width:1000px) and (max-width:1319px) 329px, 335px" alt={movie.title}/>
          </div>
         
          </div>
          <span className="movie-title">{movie.title}</span>
      </li>
      ))}
          


            </ul>
            </div>
        </div>
       </div>
         </div>
         
        </>
    )
}

export default AppHome;


