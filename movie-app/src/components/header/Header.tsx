import React, { useCallback, useEffect, useState } from "react";
import "./Header.css";
import { Button, Field, Label, SearchBox } from "@fluentui/react-components";
import { ListFilled, PersonFilled } from "@fluentui/react-icons";
import {
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
import axios from "axios";
import { handleAxiosError, MOVIES_API } from "../../constants/Endpoint";
import { UserContext } from "../Context/Statemanagement";




interface HeaderProps {
  onSearch: (searchTerm: string) => void;
}
const useStyles = makeStyles({
  content: {
    display: "flex",
    flexDirection: "column",
    rowGap: "10px",
  },
});


const Headers: React.FC<HeaderProps> = ({onSearch }) => {
  const [isOpen, setIsOpen] = useState(true);
  const styles = useStyles();
  const [localUser, setlocalUser] = useState<IUser>(Inituser);
  const [returnedUsers, setReturnedUser] = useState<IUser>();
  const userContext = React.useContext(UserContext);

  const postUser = useCallback(async (user: IUser): Promise<IUser | undefined> => {
    try {
      const response = await axios.post<IUser>(`${MOVIES_API}/Users`, user);
      return response.data;
    } catch (error) {
      if (axios.isAxiosError(error)) {
        handleAxiosError(error);
      } else {
        console.error(`Unexpected error: ${error}`);
      }
    }
  }, []);
  
  const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const { id, value } = event.target;
    setlocalUser({ ...localUser, [id]: value });
  };

  const HandleSubmit = useCallback(async () => {
    if(localUser.phone){
      const result = await postUser(localUser);
      if (result) {
        setReturnedUser(result);
        userContext?.setUser(result);
        setIsOpen(false);
      }
    }
  }, [postUser, localUser]);

  const handleSearch = (event: any, data: any) => {
    onSearch(data.value);
  };

  useEffect(() => {
   console.log("users: ", returnedUsers);
  }, [returnedUsers]);

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
                  
                  className="mobile-nav-button"
                > I M D B</Button>
              </nav>
            </div>
            <div className="moview-nav-header__mobile-tab movies-typ-headline-emph">
              <span className="movie-text-header">I M D B</span>
            </div>
            <div className="movie-nav-header__buttons-wrapper movie-nav-header__desktop"></div>
            <nav className="movie-nav-header__desktop-links movie-nav-header__desktop">
              <ul className="movie-nav-header__desktop-links-list">
                <li>
                  <span className="movie-nav-header__link--active nav-header__link">
                    <div className="tab__icon"></div>
                    <span className="tab__title" dir="ltr">
                    {returnedUsers && returnedUsers.name ? `Hi, ${returnedUsers.name}!` : ''}
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
                {returnedUsers?.name === null? 'Sign In': returnedUsers?.name}
              </Button>
            </div>
          </div>
        </div>
      </div>
      <div>
         <Dialog open={isOpen}>
                <DialogSurface>
                    <DialogBody>
                        <DialogTitle>Welcome!</DialogTitle>
                        <DialogContent className={styles.content}>
              <Label required htmlFor={"name"}>
              Enter your Name
              </Label>
              <Input onChange={handleInputChange} required type="text" id={"name"} />
              <Label required htmlFor={"phone"}>
                Enter your Phone number 
              </Label>
              <Input onChange={handleInputChange} required type="text" id={"phone"} />
                       </DialogContent>
                        <DialogActions>
                            <Button appearance="primary" onClick={HandleSubmit}>Submit</Button>
                        </DialogActions>
                    </DialogBody>
                </DialogSurface>
            </Dialog>
         </div>
    </>
  );
};

export default Headers;
