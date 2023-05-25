import React, { useState, useEffect } from 'react';
import "./App.css";
import { BrowserRouter as Router, Routes, Route, useNavigate } from "react-router-dom";
import { AppMenu } from "./components/AppMenu";
import { AppHome } from "./components/AppHome";
import { LoginForm } from "./components/Login/login";
import { AllDestinations } from "./components/PublicDestinations/AllDestinations";
import { PrivateDestinations } from "./components/PrivateDestination/PrivateDestinations";

function App() {
    const [userid, setUserid] = useState('');

    useEffect(() => {
        const storedUserid = localStorage.getItem('userid');
        if (storedUserid) {
            setUserid(storedUserid);
        }
    }, []);

    // @ts-ignore
    function handleLogin(userid) {
        setUserid(userid);
        localStorage.setItem('userid', userid);
    }

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
