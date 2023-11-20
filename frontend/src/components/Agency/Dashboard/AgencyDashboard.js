import React, { useState, useEffect } from 'react';
import { List, ListItem, ListItemText, Typography, Checkbox, Button, Container } from '@mui/material';
import { getAllServiceOfferItem } from "../../../services/AgencyService";

const AgencyDashboard = () => {
  const [checkedItems, setCheckedItems] = useState([]);
  const [serviceOfferItems, setServiceOfferItems] = useState([]);

  useEffect(() => {
    const getAll = async () => {
      try {
        const data = await getAllServiceOfferItem();
        setServiceOfferItems(data); 
      } catch (error) {
        console.error('Greška pri dohvatanju ServiceOfferItem-a:', error);
      }
    };

    getAll();
  }, []);

  const handleSubmit = (id) => () => {
    const currentIndex = checkedItems.indexOf(id);
    const newChecked = [...checkedItems];

    if (currentIndex === -1) {
      newChecked.push(id);
    } else {
      newChecked.splice(currentIndex, 1);
    }

    setCheckedItems(newChecked);
  };

  const handleChooseItems = () => {
    const selectedItems = serviceOfferItems.filter((item) => checkedItems.includes(item.id));
    console.log('Izabrani ServiceOfferItem:', selectedItems);
    // TODO Stavljanje na localStorage i slanje na bek
  };

  return (
    <Container maxWidth="sm">
      <Typography variant="h4" align="center" gutterBottom>
        Agency Dashboard
      </Typography>
      <List>
        {serviceOfferItems.map((offer) => (
          <ListItem key={offer.id} button onClick={handleSubmit(offer.id)}>
            <ListItemText
              primary={offer.OfferName}
              secondary={`Monthly Price: ${offer.MonthlyPrice}, Yearly Price: ${offer.YearlyPrice}`}
            />
            <Checkbox checked={checkedItems.indexOf(offer.id) !== -1} />
          </ListItem>
        ))}
      </List>
      <Button variant="contained" onClick={handleChooseItems}>
        Submit
      </Button>
    </Container>
  );
};

export default AgencyDashboard;
