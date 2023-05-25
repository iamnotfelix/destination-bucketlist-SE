import logo from './logo.svg';
import './App.css';
import { BrowserRouter as Routes, Route, Router} from 'react-router-dom';
import AllDestinations from './components/AllDestinations'

function App() {
    return (
        <Router>
            <Routes>
                <Route path="/" Component={<AllDestinations />} />
                <Route path="/alldestinations" Component={<AllDestinations />} />
            </Routes>
        </Router>
    );
}

export default App;
