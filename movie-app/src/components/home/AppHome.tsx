/* eslint-disable jsx-a11y/anchor-is-valid */
import React, { useEffect, useState } from "react";
import "./AppHome.css";
import { Field, Label, SearchBox } from "@fluentui/react-components";
import { IMovies } from "../../Interface/Movies";

interface AppHomeProps {
    searchTerm: string;
  }

const AppHome: React.FC<AppHomeProps>= ({searchTerm}) => {
    const [movieTitle, setmovieTitle] = useState<any>("Explore Movies & TV shows");
    const [movies, setMovies] = useState<IMovies[]>([]);
    
    const handleSearch = (event: any, data: any) => {
        console.log("Second Search", data.value);
      };

    useEffect(() => {
        if (searchTerm) {
         console.log("serch terms: ",searchTerm );
        }
      }, [searchTerm]);
    return (
        <>
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
                {movieTitle}
            </h1>
            <ul className="movie-collection-page__grid">
                <li className="movie-collection-page__item">
                    <a href="" className="movie-editorial-item-lockup" >
                        <div className="movie-media-artwork-v2__container movie-media-artwork-v2__container-serve">
                          <img src=""  className="movie-media-artwork-v2__image" alt="Feature Films"   />
                        </div>
                    </a>
                </li>
            </ul>
            </div>
        </div>
       </div>
        </>
    )
}

export default AppHome;