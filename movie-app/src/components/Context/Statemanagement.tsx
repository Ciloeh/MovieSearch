import React from 'react';
import { IUser } from '../../Interface/Iusers';


interface UserContextType {
    user: IUser | null;
    setUser: React.Dispatch<React.SetStateAction<IUser | null>>;
  }
  
  export const UserContext = React.createContext<UserContextType | undefined>(undefined);
  