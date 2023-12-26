import { bankApi } from "../helpers/ConfigHelper";

export const payWithCard = async (creditCardInfo) => {
  return await bankApi.post(`/Banks/pay-with-card`, creditCardInfo);
};

export const generateQRCode = async (qrCodeInfo) => {
  return await bankApi.post('/Banks/generate-qr-code', qrCodeInfo);
}

export const payWithQRCode = async (qrCodeInfo) => {
  return await bankApi.post('/Banks/pay-with-qr-code', qrCodeInfo);
}