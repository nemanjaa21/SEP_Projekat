import React, { useState, useEffect } from 'react';
import { List, ListItem, ListItemText, Typography, Checkbox, Button, Container, Box } from '@mui/material';
import { getAllServiceOfferItem, createServiceOffer } from "../../../services/AgencyService";
import { useNavigate } from "react-router-dom";
import NavBar from '../../NavBar/NavBar';

const ServiceOfferItem = () => {
  const [checkedItems, setCheckedItems] = useState({});
  const [serviceOfferItems, setServiceOfferItems] = useState([]);
  const navigate = useNavigate();

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
  
  function checkOfferName(eOfferName) {
    if (eOfferName === 0) {
      return "Codification of laws";
    } else if (eOfferName === 1) {
      return "Publication of laws on internet";
    } else if (eOfferName === 2) {
      return "Issuance of laws printed form";
    } else if (eOfferName === 3) {
      return "Issuance of laws electronic form";
    } else {
      return "Unknown";
    }
  }

  const handleCheckedItems = (id, isChecked) => () => {
    setCheckedItems((prevCheckedItems) => ({
      ...prevCheckedItems,
      [id]: isChecked,
    }));
  };

  const handleSubmit = async () => {
    try {
      const response = await createServiceOffer(checkedItems);
      localStorage.setItem("serviceOfferId", response.data.id);
      navigate("/pspDashboard");

    } catch (error) {
      console.error('Greška pri dohvatanju ServiceOfferItem-a:', error);
    }
  };

  return (
    <>
    <NavBar />  
      <Box
      sx={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        height: "100vh",
        backgroundColor: "#243b55"
      }}>
        <Container maxWidth="sm">
          <Typography variant="h4" align="center" gutterBottom sx={{color: "#ffff"}}>
            Service Offer Items
          </Typography>
          <List>
            {serviceOfferItems.map((offer) => (
              <ListItem key={offer.id}>
                <ListItemText
                  primary={checkOfferName(offer.offerName)}
                  secondary={`Monthly Price: $${offer.monthlyPrice.toFixed(2)}, Yearly Price: $${offer.yearlyPrice.toFixed(2)}`}
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
      </Box>
    </>
  );
};

export default ServiceOfferItem;
