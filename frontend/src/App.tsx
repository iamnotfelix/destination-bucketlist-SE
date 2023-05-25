import React, { useState } from "react";
import "./App.css";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { AppMenu } from "./components/AppMenu";
import { AppHome } from "./components/AppHome";
import AllDestinations from "./components/PublicDestinations/AllDestinations";

function App() {
  return (

    <React.Fragment>
      <Router>
              <AppMenu />
              <Routes>
                      
                      <Route path="/" element={<AppHome />} />
                      <Route path="/alldestinations" element={<AllDestinations />} />

                      
                      {/* volunteerings routes */}

              </Routes>
      </Router>
    </React.Fragment>
  );
}

export default App;