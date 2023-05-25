import React, { useEffect } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';

const DeleteDestination = () => {
  const location = useLocation();
  const { destination } = location.state || {};

  const navigate = useNavigate();

  useEffect(() => {
    if (destination) {
      deleteDestination();
    }
  }, [destination]);

  const deleteDestination = async () => {
    try {
      const response = await fetch(`http://localhost:5145/api/PublicDestinations/${destination.id}`, {
        method: 'DELETE',
      });

      if (response.ok) {
        console.log('Destination deleted successfully');
        // Perform any necessary actions after successfully deleting the destination
        navigate('/alldestinations'); // Redirect to the desired page
      } else {
        console.error('Error deleting destination');
      }
    } catch (error) {
      console.error('Error deleting destination:', error);
    }
  };

  return (
    <div>
      <h1>Delete Destination</h1>
      <p>Are you sure you want to delete this destination?</p>
    </div>
  );
};

export default DeleteDestination;
