import React from "react";
import "./Loader.css";
import { Spinner } from "@fluentui/react-components";


const Loader:React.FC = () => {
    return(
        <>
                <div className="absolute-shadow">
        <div className="loades">
        <Spinner size="medium"  />
        </div>
        </div>
        </>
    )
}

export default Loader;