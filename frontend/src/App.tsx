import React, { useState } from "react";
import "./App.css";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { AppMenu } from "./components/AppMenu";
import { AppHome } from "./components/AppHome";
import { AllDestinations } from "./components/PublicDestinations/AllDestinations";
import { PrivateDestinations } from "./components/PrivateDestination/PrivateDestinations";
import LoginForm from "./components/Login/Login";

function App() {
  return (

    <React.Fragment>
      <Router>
              <AppMenu />
              <Routes>
                      
                      <Route path="/" element={<AppHome />} />
                      <Route path="/alldestinations" element={<AllDestinations />} />


                      {/* private destination routes */}
                      <Route path="/privatedestinations" element ={<PrivateDestinations />} />


                      <Route path="/login" element ={<LoginForm />} />

              </Routes>
      </Router>
    </React.Fragment>
  );
}

export default App;