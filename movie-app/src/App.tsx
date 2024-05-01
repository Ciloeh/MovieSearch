import React, { useState } from 'react';
import './App.css';
import Header from './components/header/Header';
import { FluentProvider, webLightTheme } from '@fluentui/react-components';
import AppHome from './components/home/AppHome';
import Footer from './components/Footer/Footer';
function App() {
  const [searchTerm, setSearchTerm] = useState('');
  return (
    <div className="App">
     <FluentProvider theme={webLightTheme}>
        <Header onSearch={setSearchTerm} />
        <AppHome searchTerm={searchTerm} />
       <Footer />
      </FluentProvider>
      </div>
  );
}

export default App;
