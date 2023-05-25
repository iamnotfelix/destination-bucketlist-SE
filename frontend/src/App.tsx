import React, { useState } from "react";
import "./App.css";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { AppMenu } from "./components/AppMenu";
import { AppHome } from "./components/AppHome";
import AllDestinations from "./components/PublicDestinations/AllDestinations";
import AddDestination from "./components/PublicDestinations/AddDestination";
import UpdateDestination from "./components/PublicDestinations/UpdateDestination";
import AddPrivate from "./components/PrivateDestinations/AddPrivate";
import PickPublic from "./components/PrivateDestinations/PickPublic";

function App() {
  return (

    <React.Fragment>
      <Router>
              <AppMenu />
              <Routes>
                      
                      <Route path="/" element={<AppHome />} />
                      <Route path="/alldestinations" element={<AllDestinations />} />
                      <Route path="/adddestination" element={<AddDestination />} />
                      <Route path="/updatedestination" element={<UpdateDestination />} />
                      <Route path="/addprivatedestination" element={<AddPrivate />} />
                      <Route path="/pickpublic" element={<PickPublic />} />


                      
                      {/* volunteerings routes */}

              </Routes>
      </Router>
    </React.Fragment>
  );
}

export default App;