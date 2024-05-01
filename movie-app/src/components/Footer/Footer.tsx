import React  from "react";
import "./Footer.css";

const Footer:React.FC= () => {
    return (
        <>
        <div className="Movie-footer">
        <div className="movie-footer__left">
            <p className="clr-secondary-text">
                Copyright © 2024
                <a  href="/" target="_blank" >
                   Movie Review.
                </a>
                All rights reserved.
            </p>
            <p className="clr-primary-text movie-footer__links">
                <a className="moview-footer-link-links" target="_blank" href="https://www.imdb.com/conditions?ref_=ft_cou" rel="noopener noreferrer">
                    IMDB Service Terms
                </a>
                <a  className="moview-footer-link-links" target="_blank" href="https://www.imdb.com/privacy/redirect/?ref_=ft_redir" rel="noopener noreferrer">
                   IMDB &amp; Privacy
                </a>
                <a  className="moview-footer-link-links" target="_blank" href="#" rel="noopener noreferrer">
                    Cookie&nbsp;Policy
                </a>
                <a data-link-to-exclude="" className="moview-footer-link-links" target="_blank" href="https://help.imdb.com/imdb" rel="noopener noreferrer">
                    Support
                </a>
            </p>
        </div>
        </div>
        </>
    )
}

export default Footer;