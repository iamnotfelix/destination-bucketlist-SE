import React, { useEffect, useState } from 'react';
import { Table } from 'antd';
import { Destination } from '../../Destination';


const AllDestinations = () => {
  const [destinations, setDestinations] = useState<Destination[]>([]);

  useEffect(() => {
    const fetchDestinations = async () => {
      try {
        const response = await fetch('http://localhost:5145/api/PublicDestinations');
        const data = await response.json();
        setDestinations(data);
      } catch (error) {
        console.error('Error fetching destinations:', error);
      }
    };

    fetchDestinations();
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
    // Add more columns as needed
  ];

  return (
    <div>
      <h1 style={{ fontSize: '24px', marginTop: '0px' }}>All Destinations</h1>
      <div style={{ width: '200%', margin: 'auto' }}>
        <Table dataSource={destinations} columns={columns} rowKey="id" />
      </div>
    </div>
  );
};

export default AllDestinations;
