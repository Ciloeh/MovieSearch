import React from "react";
import "./Header.css";
import { Button, Field, SearchBox } from "@fluentui/react-components";
import { ListFilled, PersonFilled } from "@fluentui/react-icons";


interface HeaderProps {
  onSearch: (searchTerm: string) => void;
}


const Headers: React.FC<HeaderProps> = ({onSearch }) => {

  const handleSearch = (event: any, data: any) => {
    onSearch(data.value);
  };

  return (
    <>
      <div className="movies_nav-header movies_menucl">
        <div className="movies-header-wrapper">
          <div className="movies-header-content">
            <div className="movies-header__dropdown-menu">
              <nav
                className="movies-header__dropdown-menu-trigger"
                id="mobile-navigator"
              >
                <Button
                  icon={<ListFilled />}
                  className="mobile-nav-button"
                ></Button>
              </nav>
            </div>
            <div className="moview-nav-header__mobile-tab movies-typ-headline-emph">
              <span className="movie-text-header">Search Movies</span>
            </div>
            <div className="movie-nav-header__buttons-wrapper movie-nav-header__desktop"></div>
            <nav className="movie-nav-header__desktop-links movie-nav-header__desktop">
              <ul className="movie-nav-header__desktop-links-list">
                <li>
                  <span className="movie-nav-header__link--active nav-header__link">
                    <div className="tab__icon"></div>
                    <span className="tab__title" dir="ltr">
                    Hi! Jude, 
                    </span>
                  </span>
                </li>
                <li>
                  <span className="nav-header__link">
                    <div className="tab__icon"></div>
                    <span className="tab__title" dir="ltr">
                    Search your favorite movies
                    </span>
                  </span>
                </li>
              </ul>
            </nav>
            <div className="movie-nav-header__user-controls">
              <div className="movie-search-header__search movie-nav-header__desktop">
                <div className="movie-search-header__search movie-search-header__search-page">
                  <label
                  
                    id="search-header-form-input-label"
                    className="movie-search-header__form-input-label"
                  >
                    Search
                  </label>
                  <div role="search">
                    <div className="movie-search-header__search-input-wrapper">
                    <Field className="">
                        <SearchBox  placeholder="Search Movie.." appearance="filled-darker"  onChange={handleSearch} />
                    </Field>
                    </div>
                    <button
                      aria-label="Clear text"
                      className="search-header__button search-header__button--close"
                      type="reset"
                    >
                      {/* icon */}
                    </button>
                  </div>

                  <ul className="search-hints is-hidden"></ul>
                </div>
              </div>
              <Button icon={<PersonFilled />} className="movie-user-button" type="button">
                Sign In
              </Button>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default Headers;
