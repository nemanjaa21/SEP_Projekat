import { pspApi } from "../helpers/ConfigHelper";

export const creditCardPayment = async () => {
  return await pspApi.get(`/card-payment`);
};

export const bitcoinPayment = async () => {
  return await pspApi.get(`/bitcoin-payment`);
};

export const qrCodePayment = async () => {
  return await pspApi.get(`/qrcode-payment`);
};

export const payPalPayment = async () => {
  return await pspApi.get(`/paypal-payment`);
};

//POTREBNO OMOGUCITI AGENCIJAMA DA SE DODATNO PRIJAVLJUJU ILI SKIDAJU PAYMENTE U PSP-u