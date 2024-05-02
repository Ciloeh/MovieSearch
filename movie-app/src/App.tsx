import React, { useState } from 'react';
import './App.css';
import Header from './components/header/Header';
import { FluentProvider, webLightTheme } from '@fluentui/react-components';
import AppHome from './components/home/AppHome';
import Footer from './components/Footer/Footer';
import { BrowserRouter as Router, Route,  Routes } from 'react-router-dom';
import SingleView from './components/home/SingleView';
import { UserContext } from './components/Context/Statemanagement';
import { IUser } from './Interface/Iusers';

function App() {
  const [searchTerm, setSearchTerm] = useState('');
  const [user, setUser] = useState<IUser | null>(null);
  return (
    <UserContext.Provider value={{ user, setUser }}>
    <Router>
      <div className="App">
        <FluentProvider theme={webLightTheme}>
          <Header onSearch={setSearchTerm} />
          <Routes>
            <Route path="/single-view/:movieTitle" element={<SingleView />} />
            <Route path="/" element={<AppHome searchTerm={searchTerm} />} />
          </Routes>
          <Footer />
        </FluentProvider>
      </div>
    </Router>
    </UserContext.Provider>
  );
}

export default App;
