import React, { useContext, useState, useEffect, useCallback, useRef } from 'react';
import {
    makeStyles,
    Body1,
    Caption1,
    Button,
    shorthands,
    tokens,
    Rating,
  } from "@fluentui/react-components";
  import { ArrowReplyRegular, MoreHorizontal20Regular, ShareRegular } from "@fluentui/react-icons";
  import {
    Card,
    CardFooter,
    CardHeader,
    CardPreview,
  } from "@fluentui/react-components";
import "./AppHome.css";
import { IMovies } from "../../Interface/Movies";
import { useParams } from 'react-router-dom';
import { handleAxiosError, MOVIES_API } from "../../constants/Endpoint";
import axios from "axios";
import { UserContext } from "../Context/Statemanagement";
import Loader from '../../constants/Loader/Loader';

  const useStyles = makeStyles({
    main: {
      ...shorthands.gap("16px"),
      display: "flex",
      flexWrap: "wrap",
    },
  
    card: {
      width: "100%",
      maxWidth: "550px",
      height: "fit-content",
    },
  
    caption: {
      color: tokens.colorNeutralForeground3,
    },
  
    smallRadius: {
      ...shorthands.borderRadius(tokens.borderRadiusSmall),
    },
  
    grayBackground: {
      backgroundColor: tokens.colorNeutralBackground3,
    },
  
    logoBadge: {
      ...shorthands.padding("5px"),
      ...shorthands.borderRadius(tokens.borderRadiusSmall),
      backgroundColor: "#FFF",
      boxShadow:
        "0px 1px 2px rgba(0, 0, 0, 0.14), 0px 0px 2px rgba(0, 0, 0, 0.12)",
    },
   
    
      title: {
        ...shorthands.margin(0, 0, "12px"),
      },
    
      description: {
        ...shorthands.margin(0, 0, "12px"),
      },
    
      text: {
        ...shorthands.margin(0),
      },
  });

const SingleView: React.FC = () => {
     const styles = useStyles();
     const [SingleView, setSingleView] = useState<IMovies>();
     const usercontext = useContext(UserContext);
     const { movieTitle } = useParams();
     const [IsLoader, setIsLoader] = useState<boolean>(false);
     const hasSaved = useRef(false);
     const fetchSingleMovieByTitle = useCallback(async (title: string) => {
        try {
          const response = await axios.get<IMovies>(`${MOVIES_API}/Movies/${title}`);
          return response.data;
        } catch (error) {
          if (axios.isAxiosError(error)) {
            handleAxiosError(error);
          } else {
            console.error(`Unexpected error: ${error}`);
          }
        }
      }, []);
    
     
      const fetchSingleMovies = useCallback(async (data: any) => {
        const result = await fetchSingleMovieByTitle(data);
        if (result) {
            setSingleView(result);
        }
      }, [fetchSingleMovieByTitle]);


    
      




      useEffect(() => {
       if(movieTitle){
       
        fetchSingleMovies(movieTitle);
       }
    
      }, [fetchSingleMovies,SingleView, movieTitle]);
    
    return(
        <>
        <div className="Single-view">
        <Card className={styles.card}>
  <CardPreview>
    <img
      src={SingleView?.poster}
      alt={SingleView?.title}
      height="450"
    />
  </CardPreview>

  <CardHeader
    header={
      <Body1>
        <b>{SingleView?.title} ({SingleView?.year})</b>
      </Body1>
    }
    description={<Caption1>Directed by {SingleView?.director}</Caption1>}
    action={
      <Button
        appearance="transparent"
        icon={<MoreHorizontal20Regular />}
        aria-label="More options"
      />
    }
  />

  <p className={styles.text}>
   {SingleView?.plot}
  </p>

  <CardFooter>
    <p>Rating: {SingleView?.imdbRating}</p>
    <Rating step={0.5}  defaultValue={SingleView?.imdbRating? parseFloat(SingleView.imdbRating) : undefined} />
    <div style={{ display: 'flex', flexDirection: 'column' }}>
  <span>Released: {SingleView?.released}</span>
  <span>Runtime: {SingleView?.runtime}</span>
  <span>Language: {SingleView?.language}</span>
  <span>Country: {SingleView?.country}</span>
  <span>Awards: {SingleView?.awards}</span>
</div>

  </CardFooter>
</Card>
</div>


        </>
    )
}

export default SingleView;