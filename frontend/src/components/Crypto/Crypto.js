import { useState } from "react";
import { Button } from "@mui/material";
import { createEthereumPayment } from "../../services/CryptoService.js"
import { getImageLink } from "../../../services/userService";
import { useNavigate } from "react-router-dom";


return (
    <Button
        onClick={async () => {

            await createEthereumPayment(data.id);

        }}
        color="info"
        variant="contained"
        sx={{ minHeight: "45px", marginLeft: "10px" }}
    >
        {/* <img src="metamask.png" alt="metamask" style={{ width: "100px" }} /> */}
    </Button>
);