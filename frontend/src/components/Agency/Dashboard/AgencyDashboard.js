import React, { useState, useEffect } from 'react';
import { List, ListItem, ListItemText, Typography, Checkbox, Button, Container } from '@mui/material';
import { getAllServiceOfferItem, createServiceOffer } from "../../../services/AgencyService";
import { useNavigate } from "react-router-dom";

const AgencyDashboard = () => {
  const [checkedItems, setCheckedItems] = useState({});
  const [serviceOfferItems, setServiceOfferItems] = useState([]);
  const navigate = useNavigate();

  function checkOfferName(eOfferName) {
    console.log(eOfferName);
    if (eOfferName == 0) {
      return "Prva opcija";
    } else if (eOfferName == 1) {
      return "Druga opcija";
    } else if (eOfferName == 2) {
      return "Treća opcija";
    } else if (eOfferName == 3) {
      return "Četvrta opcija";
    } else {
      return "Nepostojeća opcija";
    }
  }

  
  useEffect(() => {
    const getAll = async () => {
      try {
        const response = await getAllServiceOfferItem();
        setServiceOfferItems(response.data);
      } catch (error) {
        console.error('Greška pri dohvatanju ServiceOfferItem-a:', error);
      }
    };

    getAll();
  }, []);

  const handleCheckedItems = (id, isChecked) => () => {
    setCheckedItems((prevCheckedItems) => ({
      ...prevCheckedItems,
      [id]: isChecked,
    }));
  };

  const handleSubmit = async () => {
    try {
      console.log("napravio", checkedItems);
      const response = await createServiceOffer(checkedItems);
      console.log("vratio", response.data);
      localStorage.setItem("ServiceOfferItem", response.data.id);
      navigate("/pspDashboard");

    } catch (error) {
      console.error('Greška pri dohvatanju ServiceOfferItem-a:', error);
    }
    //const selectedItems = serviceOfferItems.filter((item) => checkedItems[item.id] !== null);
    //console.log('Izabrani ServiceOfferItem:', selectedItems);
    // Implementirajte logiku za čuvanje izabranih stavki u localStorage
    //localStorage.setItem("ServiceOfferItem", selectedItems);
  };

  return (
    <Container maxWidth="sm">
      <Typography variant="h4" align="center" gutterBottom>
        Agency Dashboard
      </Typography>
      <List>
        {serviceOfferItems.map((offer) => (
          <ListItem key={offer.id}>
            <ListItemText
              primary={checkOfferName(offer.offerName)}
              secondary={`Monthly Price: ${offer.monthlyPrice}, Yearly Price: ${offer.yearlyPrice}`}
            />
            <div>
              <Checkbox
                checked={checkedItems[offer.id] === true}
                onChange={() => handleCheckedItems(offer.id, true)()}
              />
              <label>Monthly Subscription</label>
            </div>
            <div>
              <Checkbox
                checked={checkedItems[offer.id] === false}
                onChange={() => handleCheckedItems(offer.id, false)()}
              />
              <label>Yearly Subscription</label>
            </div>
          </ListItem>
        ))}
      </List>
      <Button variant="contained" onClick={handleSubmit}>
        Submit
      </Button>
    </Container>
  );
};

export default AgencyDashboard;
