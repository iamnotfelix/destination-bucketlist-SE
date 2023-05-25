import React, { useState, useEffect } from 'react';
import "./App.css";
import { BrowserRouter as Router, Routes, Route, useNavigate } from "react-router-dom";
import { AppMenu } from "./components/AppMenu";
import { AppHome } from "./components/AppHome";
import { LoginForm } from "./components/Login/login";

function App() {
    const [userid, setUserid] = useState('');

    useEffect(() => {
        const storedUserid = localStorage.getItem('userid');
        if (storedUserid) {
            setUserid(storedUserid);
        }
    }, []);

    function handleLogin(userid) {
        setUserid(userid);
        localStorage.setItem('userid', userid);
    }

    function handleLogout() {
        setUserid('');
        localStorage.removeItem('userid');
    }

    return (
        <React.Fragment>
            <Router>
                <AppMenu userid={userid} handleLogout={handleLogout}/>
                <Routes>
                    <Route path="/" element={<AppHome />} />
                    <Route path="/login" element={<LoginForm handleLogin={handleLogin} />} />
                </Routes>
            </Router>
        </React.Fragment>
    );
}
export default App;
