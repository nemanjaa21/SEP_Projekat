import React, { useEffect, useState } from "react";
import { generateQRCode, payWithQRCode } from "../../services/BankService";

const QRCode = () => {
  const [qrImage, setQRImage] = useState("");

  useEffect(() => {
    const paymentId = sessionStorage.getItem("PaymentId");

    const generateQRCodeDTO = {
      MerchantId: 3,
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
    <div>
      {qrImage && (
        <div>
          <h1>QR Code</h1>
          <img src={qrImage} alt="QR Code" />
        </div>
      )}
    </div>
  );
};

export default QRCode;
