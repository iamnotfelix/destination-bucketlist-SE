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

export const AllDestinations = () => {

  const[loading, setLoading] = useState(true)
  const[destinations, setDestinations] = useState([]);

  useEffect(() => {
      fetch(`http://localhost:5145/api/PublicDestinations`)
          .then(res => res.json())
          .then(data => {setDestinations(data); setLoading(false);})
  }, []);

  const columns = [
    {
      title: 'Title',
      dataIndex: 'title',
      key: 'title',
    },
    {
      title: 'Image',
      dataIndex: 'image',
      key: 'image',
      render: (image: string) => <img src={image} alt="Destination" height={50} />,
    },
    {
      title: 'Description',
      dataIndex: 'description',
      key: 'description',
    },
    {
      title: 'Geolocation',
      dataIndex: 'geolocation',
      key: 'geolocation',
    },
    {
      title: 'Actions',
      key: 'actions',
      render: (text: any, destination: Destination) => (
        <div>
          <Button
            type="primary"
            onClick={() => handleUpdateDestination(destination)}
            style={{ marginRight: '8px' }}
          >
            Update
          </Button>
          <Button type="primary" onClick={() => handleDeleteDestination(destination.id)}>
            Delete
          </Button>
        </div>
      ),
    }
    // Add more columns as needed
  ];
  console.log(destinations);

  const handleAddDestination = () => {
    navigate('/adddestination');
  };

  const handleUpdateDestination = (destination: Destination) => {
    navigate('/updateDestination', { state: { destination } });
  };
  
  const handleDeleteDestination = async (destinationId: number) => {
    try {
      const response = await fetch(`http://localhost:5145/api/PublicDestinations/${destinationId}`, {
        method: 'DELETE',
      });
  
      if (response.ok) {
        console.log('Destination deleted successfully');
        // Perform any necessary actions after successfully deleting the destination
        // Fetch the updated list of destinations or update the state accordingly
        const updatedDestinations = destinations.filter(destination => destination.id !== destinationId);
        setDestinations(updatedDestinations);
      } else {
        console.error('Error deleting destination');
      }
    } catch (error) {
      console.error('Error deleting destination:', error);
    }
  };
  

  return (
    <div>
      <h1 style={{ fontSize: '24px', marginTop: '0px' }}>All Destinations</h1>
      <div style={{ width: '80%', margin: 'auto' }}>
        <Table dataSource={destinations} columns={columns} rowKey="id" />
      </div>
      <Button type="primary" onClick={handleAddDestination} style={{ marginTop: '20px' }}>
        Add Destination
      </Button>
    </div>

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

export default AllDestinations;