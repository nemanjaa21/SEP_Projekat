import React, { useEffect, useState } from "react";
import { generateQRCode } from "../../services/BankService";
import {
  Container,
  Box
} from "@mui/material";
import NavBar from "../NavBar/NavBar";

const QRCode = () => {
  const [qrImage, setQRImage] = useState("");

  useEffect(() => {
    const paymentId = sessionStorage.getItem("PaymentId");

    const generateQRCodeDTO = {
      MerchantId: 1,
      UserId: 1,
      Currency: 0,
      PaymentId: paymentId,
    };

    const fetchQRCode = async () => {
      try {
        const response = await generateQRCode(generateQRCodeDTO);
        if (response.data && response.data.imageBase64) {
          setQRImage(`data:image/png;base64,${response.data.imageBase64}`);
        }
      } catch (error) {
        console.error("Error:", error);
      }
    };

    fetchQRCode();
  }, []);

  return (
    <>
    <NavBar />
      <Box sx={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        height: "100vh",
        backgroundColor: "#243b55",
      }}>
        <Container maxWidth="sm">
          <div>
            {qrImage && (
              <div>
                <h1 style={{marginLeft: "126px"}}>QR Code</h1>
                <img src={qrImage} alt="QR Code" style={{width: "400px", height: "400px"}}/>
              </div>
            )}
          </div>
        </Container>
      </Box>
    </>
  );
};

export default QRCode;
