import { Button, Typography, Box, Container } from "@mui/material";
import { payPalPayment } from "../../../services/paymentService";
import { useNavigate } from "react-router-dom";

const PaypalPayment = () => {
    const navigate = useNavigate();

    const handleClose = () => {
        navigate("/pspDashboard");
    }

    const processPayPalPayment = async (id) => {
        try {
           const response = await payPalPayment(id); 
        }
        catch(error) {
            console.log("Error processing PaypalPayment: ", error);
        }
    }

    return (
        <>
            <Box sx={{
                display: "flex",
                justifyContent: "center",
                alignItems: "center",
                height: "100vh",
                backgroundColor: "#243b55",
            }}>
                <Container maxWidth="sm">
                    <Typography variant="h4" align="center" gutterBottom>
                        PayPal Payment
                    </Typography>
                    <Button
                        onClick={() => processPayPalPayment(data.id)}
                        sx={{ background: "yellow", minHeight: "20px" }}
                        variant="contained"
                    >
                        <img src="paypal.png" alt="paypal" style={{ width: "100px" }} />
                    </Button>
                    <br />
                    <br />
                    <Button
                        onClick={() => createPayPalPayment(data.id)}
                        color="info"
                        variant="contained"
                        sx={{ minHeight: "45px" }}
                    >
                        <img src="metamask.png" alt="metamask" style={{ width: "100px" }} />
                    </Button>
                    <Button variant="contained" color="secondary" onClick={handleClose}>Cancel</Button>
                </Container>
            </Box>
        </>
    );
};

export default PaypalPayment;