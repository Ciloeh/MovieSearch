import { AxiosError } from "axios";

export const MOVIES_API = 'https://localhost:44377/api';

//export const MOVIES_API = "https://moviesearch.azurewebsites.net/api"



export const handleAxiosError = (error: AxiosError) => {
  if (error.response) {
    console.error(`Server responded with status code ${error.response.status}`);
    console.error(`Response data: ${error.response.data}`);
  } else if (error.request) {
    console.error(`No response received: ${error.request}`);
  } else {
    console.error(`Error setting up request: ${error.message}`);
  }
}