import React, { useState, useEffect } from 'react';
import { List, ListItem, ListItemText, Typography, Checkbox, Button, Box, Container } from '@mui/material';
import NavBar from '../../NavBar/NavBar';
import { getAllPaymentServices, subscribeOnPaymentServices } from "../../../services/AgencyService";

const PaymentServices = () => {
    const [services, setServices] = useState([]);

    useEffect(() => {
        const getAll = async () => {
            try {
                const response = await getAllPaymentServices(1);
                setServices(response.data);
            } catch (error) {
                console.error('Greška pri dohvatanju PaymentServices-a:', error);
            }
        };

        getAll();
    }, []);

    const handleCheckboxChange = (index) => () => {
        const updatedServices = [...services];
        updatedServices[index].subscribed = !updatedServices[index].subscribed;
        setServices(updatedServices);
    };

    const handleSubscribe = async () => {
        try {
            const response = await subscribeOnPaymentServices(services, 1);
            setServices(response.data);
        } catch (error) {
            console.error('Greška pri slanju PaymentServices-a:', error);
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
                    backgroundColor: "#243b55",
                }}>
                <Container maxWidth="sm">
                    <Typography variant="h4" align="center" gutterBottom sx={{ color: "#ffff" }}>
                        Payment Services
                    </Typography>
                    <List>
                        {services.map((service, index) => (
                            <ListItem key={service.id} disablePadding>
                                <Checkbox sx={{ color: "#ffff" }}
                                    checked={service.subscribed}
                                    onChange={handleCheckboxChange(index)}
                                />
                                <ListItemText sx={{ color: "#ffff" }} primary={service.name} />
                            </ListItem>
                        ))}
                    </List>
                    <Button variant="contained" onClick={handleSubscribe}>Subscribe</Button>
                </Container>
            </Box>
        </>
    );
};

export default PaymentServices;
