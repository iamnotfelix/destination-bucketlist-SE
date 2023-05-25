import {
    TableContainer,
    Paper,
    Table,
    TableHead,
    TableRow,
    TableCell,
    TableBody,
    CircularProgress,
    Container,
    IconButton,
    Tooltip,
  } from "@mui/material";
  
  import EditIcon from "@mui/icons-material/Edit";
  import DeleteIcon from '@mui/icons-material/Delete';
  import { useEffect, useState } from "react";
  import { Destination } from "../../Destination";
  import { Link } from "react-router-dom";
  import AddIcon from "@mui/icons-material/Add";
  import VisibilityIcon from '@mui/icons-material/Visibility';
  
  export const PrivateDestinations = () => {
  
    const[loading, setLoading] = useState(true)
    const[destinations, setDestinations] = useState([]);
  
    useEffect(() => {
        fetch(`http://localhost:5145/api/PublicDestinations`)
            .then(res => res.json())
            .then(data => {setDestinations(data); setLoading(false);})
    }, []);
  
    console.log(destinations);
  
    return (
  
        <Container>
            <h1 style={{marginTop:"65px", color:'black'}}>All Destinations</h1>
  
            {loading && <CircularProgress />}
  
            {!loading && destinations.length == 0 && <div>No destinations found</div>}
  
            {!loading && destinations.length > 0 && (
  
                <TableContainer component={Paper}>
                    <Table sx={{ minWidth: 800 }} aria-label="simple table" style={{backgroundColor:"whitesmoke"}}>
                        <TableHead>
                            <TableRow>
                                <TableCell align="center" style={{color:"black", fontWeight:'bold'}}>Crt.</TableCell>
                                <TableCell align="center" style={{color:"black", fontWeight:'bold'}}>Title</TableCell>
                                <TableCell align="center" style={{color:"black", fontWeight: 'bold'}}>Image</TableCell>
                                <TableCell align="center" style={{color:"black", fontWeight: 'bold'}}>Description</TableCell>
                                <TableCell align="center" style={{color:"black", fontWeight: 'bold'}}>Geolocation</TableCell>
  
                                <TableCell align="center" style={{color:"black", fontWeight: 'bold'}}>Operations
                                    <IconButton component={Link} sx={{ mr: 3 }} to={`/shelter/add`}>
                                        <Tooltip title="Add a new destination" arrow>
                                            <AddIcon style={{color:"black", fontSize:"20px"}} />
                                        </Tooltip>
                                    </IconButton></TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {destinations.map((destination:Destination, index) => (
                                <TableRow key={destination.id}>
                                    <TableCell component="th" scope="row">
                                        {index + 1}
                                    </TableCell>
                                    <TableCell align="center">
                                        {destination.title}
                                        </TableCell>
                                    <TableCell align="center">
                                            <img src={destination.image} alt="Destination" style={{ width: 100, height: 100 }} />
                                    </TableCell>
                                    <TableCell align="center">{destination.description}</TableCell>
                                    <TableCell align="center">{destination.geolocation}</TableCell>
                                    <TableCell align="center">
  
                                        <IconButton component={Link} sx={{ mr: 3 }} to={`/shelter/${destination.id}/edit`}>
                                            <EditIcon sx={{ color: "navy" }}/>
                                        </IconButton>
  
                                        <IconButton component={Link} sx={{ mr: 3 }} to={`/shelter/${destination.id}/delete`}>
                                            <DeleteIcon sx={{ color: "darkred" }} />
                                        </IconButton>
                                    </TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
            )
            }
        </Container>
  
    );
  };
  
  export default PrivateDestinations;