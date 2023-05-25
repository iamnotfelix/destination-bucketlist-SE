import React, { useState, useEffect } from "react";
import axios from "axios";
import { CircularProgress } from '@mui/material';


const AllDestinations = () => {
    const [loading, setLoading] = useState(false);
    const [destinations, setDestinations] = useState([]);

    useEffect(() => {
        setLoading(true);
        axios
            .get("htpp://localhost:5145/api/PrivateDestination") // Replace with your backend API endpoint
            .then((response) => {
                console.log(response.data);
                setDestinations(response.data);
                setLoading(false);
            })
            .catch((error) => {
                console.error(error);
                setLoading(false);
            });
    }, []);

    return (
        <div>
            <h1>All destinations</h1>

            {loading && <CircularProgress />}
            {!loading && destinations.length === 0 && <p>No destinations found</p>}
            {!loading && destinations.length > 0 && (
                <table>
                    <thead>
                        <tr>
                            <th>#</th>
                            <th>Geolocation</th>
                            <th>Title</th>
                            <th>Image</th>
                            <th>Description</th>
                            <th>Start Date</th>
                            <th>End Date</th>
                        </tr>
                    </thead>
                    <tbody>
                        {destinations.map((destination, index) => (
                            <tr key={destination.id}>
                                <td>{index + 1}</td>
                                <td>{destination.geolocation}</td>
                                <td>{destination.title}</td>
                                <td>
                                    <img src={destination.image} alt="Destination" />
                                </td>
                                <td>{destination.description}</td>
                                <td>{destination.startDate}</td>
                                <td>{destination.endDate}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            )}
        </div>
    );
};

export default AllDestinations;
