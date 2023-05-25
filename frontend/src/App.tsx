import React from 'react';
import "./App.css";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { AppMenu } from "./components/AppMenu";
import { AppHome } from "./components/AppHome";
import { LoginForm } from "./components/Login/login";
import { AllDestinations } from "./components/PublicDestinations/AllDestinations";
import { PrivateDestinations } from "./components/PrivateDestination/PrivateDestinations";
import AddDestination from "./components/PublicDestinations/AddDestination";
import UpdateDestination from "./components/PublicDestinations/UpdateDestination";

function App() {

    return (
        <React.Fragment>
            <Router>
                <AppMenu />
                <Routes>
                    <Route path="/" element={<AppHome />} />
                    <Route path="/alldestinations" element={<AllDestinations />} />
                    <Route path="/privatedestinations" element ={<PrivateDestinations />} />
                    <Route path="/login" element ={<LoginForm />} />

                    <Route path="/adddestination" element={<AddDestination />} />
                    <Route path="/updatedestination" element={<UpdateDestination />} />
                </Routes>
            </Router>
        </React.Fragment>
    );
}
export default App;
