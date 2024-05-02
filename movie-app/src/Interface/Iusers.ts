import { ILatestSearch } from "./LatestSearched";

export interface IUser {
    id: number;
    phone: string;
    name: string;
    dateAdded: Date;
    searchQueries:  ILatestSearch[];
  }


  export let Inituser: IUser = {
    id: 0,
    phone: '',
    name: '',
    dateAdded: new Date(),
    searchQueries: [],
  };